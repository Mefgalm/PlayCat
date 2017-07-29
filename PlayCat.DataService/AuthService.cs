using Microsoft.EntityFrameworkCore;
using PlayCat.DataService.Mappers;
using PlayCat.DataService.Request;
using PlayCat.DataService.Response;
using System;
using System.Linq;
using System.Web.Helpers;

namespace PlayCat.DataService
{
    public class AuthService : BaseService, IAuthService
    {
        private const int AuthTokenDaysExpired = 180;        

        private readonly IInviteService _inviteService;

        public AuthService(PlayCatDbContext dbContext, IInviteService inviteService) : base(dbContext)
        {
            _inviteService = inviteService;
        }

        public SignUpInResult SignUp(SignUpRequest request)
        {
            return RequestTemplate(request, (req) =>
            {
                if (!_inviteService.IsInviteValid(request.VerificationCode))
                    return ResponseFactory.With<SignUpInResult>().Fail("Verification code is wrong");

                if (_dbContext.Users.Any(x => x.Email == request.Email))
                    return ResponseFactory.With<SignUpInResult>().Fail("User with this email already registered");

                if (_dbContext.Users.Any(x => x.VerificationCode == request.VerificationCode))
                    return ResponseFactory.With<SignUpInResult>().Fail("This invite already used");

                string salt = Crypto.GenerateSalt();
                string passwordHah = Crypto.HashPassword(request.Password + salt);

                var dataUser = UserMapper.ToData.Get(request, (user) =>
                {
                    user.Id = Guid.NewGuid();
                    user.IsUploadingAudio = false;
                    user.PasswordHash = passwordHah;
                    user.PasswordSalt = salt;
                    user.RegisterDate = DateTime.Now;
                });

                var dataAuthToken = new DataModel.AuthToken()
                {
                    Id = Guid.NewGuid(),
                    DateExpired = DateTime.Now.AddDays(AuthTokenDaysExpired),
                    UserId = dataUser.Id,
                    IsActive = true,
                };

                _dbContext.AuthTokens.Add(dataAuthToken);
                _dbContext.Add(dataUser);
                _dbContext.SaveChanges();

                return ResponseFactory.With(new SignUpInResult()
                {
                    User = UserMapper.ToApi.Get(dataUser),
                    AuthToken = AuthTokenMapper.ToApi.Get(dataAuthToken),
                }).Success();
            });            
        }

        public SignUpInResult SignIn(SignInRequest request)
        {
            return RequestTemplate(request, (req) =>
            {
                DataModel.User dataUser = _dbContext.Users
                    .Include(x => x.AuthToken)
                    .FirstOrDefault(x => x.Email == request.Email);

                if (dataUser is null || !Crypto.VerifyHashedPassword(dataUser.PasswordHash, request.Password + dataUser.PasswordSalt))
                    return ResponseFactory.With<SignUpInResult>().Fail("Email or password is incorrect");

                //update token
                dataUser.AuthToken.DateExpired = DateTime.Now.AddDays(AuthTokenDaysExpired);
                dataUser.AuthToken.IsActive = true;

                _dbContext.SaveChanges();

                return ResponseFactory.With(new SignUpInResult()
                {
                    User = UserMapper.ToApi.Get(dataUser),
                    AuthToken = AuthTokenMapper.ToApi.Get(dataUser.AuthToken),
                }).Success();
            });
        }

        public BaseResult CheckToken(string token)
        {                            
            if (string.IsNullOrEmpty(token))
                return ResponseFactory.With<BaseResult>().HideInfo().Fail("Token not found", ResponseCode.InvalidToken);

            if(!Guid.TryParse(token, out Guid tokenId))
                return ResponseFactory.With<BaseResult>().HideInfo().Fail("Token wrong format", ResponseCode.InvalidToken);

            DataModel.AuthToken authToken = _dbContext.AuthTokens.FirstOrDefault(x => x.Id == tokenId);

            if(authToken is null)
                return ResponseFactory.With<BaseResult>().HideInfo().Fail("Token not found", ResponseCode.InvalidToken);

            if(authToken.DateExpired < DateTime.Now)
                return ResponseFactory.With<BaseResult>().HideInfo().Fail("Token is expired", ResponseCode.InvalidToken);

            if(!authToken.IsActive)
                return ResponseFactory.With<BaseResult>().HideInfo().Fail("Token is not active", ResponseCode.InvalidToken);

            return ResponseFactory.With<BaseResult>().Success();
        }
    }
}
