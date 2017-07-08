using PlayCat.DataService.Helpers;
using PlayCat.DataService.Request;
using PlayCat.DataService.Response;
using System;
using System.Linq;
using System.Web.Helpers;

namespace PlayCat.DataService
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IInviteService _inviteService;

        public AuthService(PlayCatDbContext dbContext, IInviteService inviteService) : base(dbContext)
        {
            _inviteService = inviteService;
        }

        public SignUpResult SignUp(SignUpRequest request)
        {
            ModelValidationResult modelValidationResult = ModelValidator.Validate(request);
            if (!modelValidationResult.Ok)
                return ResponseFactory.With(new SignUpResult()
                {
                    Errors = modelValidationResult.Errors
                }).Fail("Model is not valid");

            if (!_inviteService.IsInviteValid(request.VerificationCode))
                return ResponseFactory.With<SignUpResult>().Fail("Verification code is wrong");

            if(_dbContext.Users.Any(x => x.Email == request.Email))
                return ResponseFactory.With<SignUpResult>().Fail("User with this email already registered");

            if(_dbContext.Users.Any(x => x.VerificationCode == request.VerificationCode)) 
                return ResponseFactory.With<SignUpResult>().Fail("This invite already used");

            string salt = Crypto.GenerateSalt();
            string passwordHah = Crypto.HashPassword(request.Password + salt);

            var dataUser = Mappers.UserMapper.ToData.Get(request, (user) =>
            {
                user.Id = Guid.NewGuid();
                user.IsUploadingAudio = false;
                user.PasswordHash = passwordHah;
                user.PasswordSald = salt;
                user.RegisterDate = DateTime.Now;
            });

            var dataAuthToken = new DataModel.AuthToken()
            {
                Id = Guid.NewGuid(),
                DateExpired = DateTime.Now.AddDays(180),
                UserId = dataUser.Id,
            };

            _dbContext.AuthTokens.Add(dataAuthToken);
            _dbContext.Add(dataUser);
            _dbContext.SaveChanges();

            return ResponseFactory.With(new SignUpResult()
            {
                User = Mappers.UserMapper.ToApi.Get(dataUser),
                AuthToken = Mappers.AuthTokenMapper.ToApi.Get(dataAuthToken),
            }).Success();
        }

        public void SignIn()
        {

        }
    }
}
