using PlayCat.DataService;
using PlayCat.DataService.Request;
using PlayCat.DataService.Response;
using System.Linq;
using Xunit;

namespace PlayCat.Tests
{
    public class Auth : BaseTest
    {
        #region Sign Up

        [Fact]
        public void IsEmptyModelSignUp()
        {            
            SqlLiteDatabaseTest(options =>
            {
                var authService = _server.Host.Services.GetService(typeof(IAuthService)) as IAuthService;
                using (var context = new PlayCatDbContext(options))
                {
                    authService.SetDbContext(context);

                    SignUpInResult result = authService.SignUp(new SignUpRequest() { });

                    Assert.NotNull(result);
                    Assert.False(result.Ok);
                    Assert.Equal("Model is not valid", result.Info);
                    Assert.False(result.ShowInfo);
                    Assert.Null(result.User);
                    Assert.Null(result.AuthToken);
                    Assert.NotNull(result.Errors);
                    Assert.True(result.Errors.Any());
                }
            });
        }

        [Fact]
        public void IsNotValidKeySignUp()
        {           
            SqlLiteDatabaseTest(options =>
            {
                var authService = _server.Host.Services.GetService(typeof(IAuthService)) as IAuthService;
                var inviteService = _server.Host.Services.GetService(typeof(IInviteService)) as IInviteService;

                using (var context = new PlayCatDbContext(options))
                {
                    authService.SetDbContext(context);

                    SignUpInResult result = authService.SignUp(new SignUpRequest()
                    {
                        FirstName = "vlad",
                        LastName = "Kuz",
                        Password = "123456abc",
                        ConfirmPassword = "123456abc",
                        Email = "mefgalm@gmail.com",
                        VerificationCode = "123"
                    });

                    Assert.NotNull(result);
                    Assert.False(result.Ok);
                    Assert.Null(result.User);
                    Assert.Null(result.AuthToken);
                    Assert.Null(result.Errors);
                    Assert.Equal("Verification code is wrong", result.Info);
                    Assert.True(result.ShowInfo);
                }
            });
        }

        [Fact]
        public void IsValidModelSignUp()
        {
            SqlLiteDatabaseTest(options =>
            {
                var authService = _server.Host.Services.GetService(typeof(IAuthService)) as IAuthService;
                var inviteService = _server.Host.Services.GetService(typeof(IInviteService)) as IInviteService;

                using (var context = new PlayCatDbContext(options))
                {
                    authService.SetDbContext(context);

                    SignUpInResult result = authService.SignUp(new SignUpRequest()
                    {
                        FirstName = "vlad",
                        LastName = "Kuz",
                        Password = "123456abc",
                        ConfirmPassword = "123456abc",
                        Email = "mefgalm@gmail.com",
                        VerificationCode = inviteService.GenerateInvite(),
                    });

                    Assert.NotNull(result);
                    Assert.True(result.Ok);
                    Assert.NotNull(result.User);
                    Assert.NotNull(result.AuthToken);
                    Assert.Null(result.Errors);
                    Assert.Null(result.Info);
                }
            });                        
        }

        [Fact]
        public void IsUsedEmailSignUp()
        {            
            SqlLiteDatabaseTest(options =>
            {
                var authService = _server.Host.Services.GetService(typeof(IAuthService)) as IAuthService;
                var inviteService = _server.Host.Services.GetService(typeof(IInviteService)) as IInviteService;

                using (var context = new PlayCatDbContext(options))
                {
                    authService.SetDbContext(context);

                    string invite = inviteService.GenerateInvite();

                    SignUpInResult result = authService.SignUp(new SignUpRequest()
                    {
                        FirstName = "vlad",
                        LastName = "Kuz",
                        Password = "123456abc",
                        ConfirmPassword = "123456abc",
                        Email = "mefgalm@gmail.com",
                        VerificationCode = invite
                    });

                    SignUpInResult result2 = authService.SignUp(new SignUpRequest()
                    {
                        FirstName = "vlad",
                        LastName = "Kuz",
                        Password = "123456abc",
                        ConfirmPassword = "123456abc",
                        Email = "mefgalm@gmail.com",
                        VerificationCode = invite
                    });

                    Assert.NotNull(result2);
                    Assert.False(result2.Ok);
                    Assert.Null(result2.User);
                    Assert.Null(result2.AuthToken);
                    Assert.Null(result2.Errors);
                    Assert.Equal("User with this email already registered", result2.Info);
                    Assert.True(result2.ShowInfo);
                }
            });            
        }

        [Fact]
        public void IsUsedInviteSignUp()
        {
            SqlLiteDatabaseTest(options =>
            {
                var authService = _server.Host.Services.GetService(typeof(IAuthService)) as IAuthService;
                var inviteService = _server.Host.Services.GetService(typeof(IInviteService)) as IInviteService;

                using (var context = new PlayCatDbContext(options))
                {
                    authService.SetDbContext(context);

                    string invite = inviteService.GenerateInvite();

                    SignUpInResult result = authService.SignUp(new SignUpRequest()
                    {
                        FirstName = "vlad",
                        LastName = "Kuz",
                        Password = "123456abc",
                        ConfirmPassword = "123456abc",
                        Email = "mefgalm@gmail.com",
                        VerificationCode = invite
                    });

                    SignUpInResult result2 = authService.SignUp(new SignUpRequest()
                    {
                        FirstName = "vlad",
                        LastName = "Kuz",
                        Password = "123456abc",
                        ConfirmPassword = "123456abc",
                        Email = "mefgalm2@gmail.com",
                        VerificationCode = invite
                    });

                    Assert.NotNull(result2);
                    Assert.False(result2.Ok);
                    Assert.Null(result2.User);
                    Assert.Null(result2.AuthToken);
                    Assert.Null(result2.Errors);
                    Assert.Equal("This invite already used", result2.Info);
                    Assert.True(result2.ShowInfo);
                }
            });            
        }

        #endregion

        #region Sign In

        [Fact]
        public void IsValidModelSignIn()
        {           
            SqlLiteDatabaseTest(options =>
            {
                var authService = _server.Host.Services.GetService(typeof(IAuthService)) as IAuthService;
                var inviteService = _server.Host.Services.GetService(typeof(IInviteService)) as IInviteService;

                using (var context = new PlayCatDbContext(options))
                {
                    authService.SetDbContext(context);

                    SignUpInResult result = authService.SignUp(new SignUpRequest()
                    {
                        FirstName = "vlad",
                        LastName = "Kuz",
                        Password = "123456abc",
                        ConfirmPassword = "123456abc",
                        Email = "mefgalm@gmail.com",
                        VerificationCode = inviteService.GenerateInvite(),
                    });

                    Assert.NotNull(result);
                    Assert.True(result.Ok);
                    Assert.NotNull(result.User);
                    Assert.NotNull(result.AuthToken);
                    Assert.Null(result.Errors);
                    Assert.Null(result.Info);

                    SignUpInResult resultSignIn = authService.SignIn(new SignInRequest()
                    {
                        Email = "mefgalm@gmail.com",
                        Password = "123456abc",
                    });

                    Assert.NotNull(resultSignIn);
                    Assert.True(resultSignIn.Ok);
                    Assert.NotNull(resultSignIn.User);
                    Assert.NotNull(resultSignIn.AuthToken);
                    Assert.Null(resultSignIn.Errors);
                    Assert.Null(resultSignIn.Info);
                }
            });
        }

        [Fact]
        public void IsUserNotFoundSignIn()
        {            
            SqlLiteDatabaseTest(options =>
            {
                var authService = _server.Host.Services.GetService(typeof(IAuthService)) as IAuthService;
                var inviteService = _server.Host.Services.GetService(typeof(IInviteService)) as IInviteService;

                using (var context = new PlayCatDbContext(options))
                {
                    authService.SetDbContext(context);
                    SignUpInResult resultSignIn = authService.SignIn(new SignInRequest()
                    {
                        Email = "mefgalm@gmail.com",
                        Password = "123456abc",
                    });

                    Assert.NotNull(resultSignIn);
                    Assert.False(resultSignIn.Ok);
                    Assert.Null(resultSignIn.User);
                    Assert.Null(resultSignIn.AuthToken);
                    Assert.Null(resultSignIn.Errors);
                    Assert.Equal("Email or password is incorrect", resultSignIn.Info);
                    Assert.True(resultSignIn.ShowInfo);
                }
            });            
        }

        [Fact]
        public void IsUserFoundButWrongPasswordSignIn()
        {            
            SqlLiteDatabaseTest(options =>
            {
                var authService = _server.Host.Services.GetService(typeof(IAuthService)) as IAuthService;
                var inviteService = _server.Host.Services.GetService(typeof(IInviteService)) as IInviteService;

                using (var context = new PlayCatDbContext(options))
                {
                    authService.SetDbContext(context);

                    SignUpInResult result = authService.SignUp(new SignUpRequest()
                    {
                        FirstName = "vlad",
                        LastName = "Kuz",
                        Password = "123456abc",
                        ConfirmPassword = "123456abc",
                        Email = "mefgalm@gmail.com",
                        VerificationCode = inviteService.GenerateInvite(),
                    });

                    Assert.NotNull(result);
                    Assert.True(result.Ok);
                    Assert.NotNull(result.User);
                    Assert.NotNull(result.AuthToken);
                    Assert.Null(result.Errors);
                    Assert.Null(result.Info);

                    SignUpInResult resultSignIn = authService.SignIn(new SignInRequest()
                    {
                        Email = "mefgalm@gmail.com",
                        Password = "wrongPass1234",
                    });

                    Assert.NotNull(resultSignIn);
                    Assert.False(resultSignIn.Ok);
                    Assert.Null(resultSignIn.User);
                    Assert.Null(resultSignIn.AuthToken);
                    Assert.Null(resultSignIn.Errors);
                    Assert.Equal("Email or password is incorrect", resultSignIn.Info);
                    Assert.True(resultSignIn.ShowInfo);
                }
            });
        }        

        #endregion
    }
}
