using Microsoft.EntityFrameworkCore;
using PlayCat.DataService.Helpers;
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
            TrimStrings.Trim(request);

            ModelValidationResult modelValidationResult = ModelValidator.Validate(request);
            if (!modelValidationResult.Ok)
                return ResponseFactory.With(new SignUpInResult()
                {
                    Errors = modelValidationResult.Errors
                })
                .HideInfo()
                .Fail("Model is not valid");

            if (!_inviteService.IsInviteValid(request.VerificationCode))
                return ResponseFactory.With<SignUpInResult>().Fail("Verification code is wrong");

            if(_dbContext.Users.Any(x => x.Email == request.Email))
                return ResponseFactory.With<SignUpInResult>().Fail("User with this email already registered");

            if(_dbContext.Users.Any(x => x.VerificationCode == request.VerificationCode)) 
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
        }

        public SignUpInResult SignIn(SignInRequest request)
        {
            TrimStrings.Trim(request);

            ModelValidationResult modelValidationResult = ModelValidator.Validate(request);
            if (!modelValidationResult.Ok)
                return ResponseFactory.With(new SignUpInResult()
                {
                    Errors = modelValidationResult.Errors
                })
                .HideInfo()
                .Fail("Model is not valid");

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
        }
    }
}
