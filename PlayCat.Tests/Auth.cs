using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PlayCat.DataService;
using PlayCat.DataService.Request;
using PlayCat.DataService.Response;
using System.Linq;
using Xunit;

namespace PlayCat.Tests
{
    public class Auth
    {
        private readonly TestServer _server;

        public Auth()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<StartupTest>());
        }

        #region Sign Up

        [Fact]
        public void IsEmptyModelSignUp()
        {
            var authService = _server.Host.Services.GetService(typeof(IAuthService)) as IAuthService;

            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<PlayCatDbContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new PlayCatDbContext(options))
                {
                    context.Database.EnsureCreated();
                }

                // Run the test against one instance of the context
                using (var context = new PlayCatDbContext(options))
                {
                    authService.SetDbContext(context);

                    SignUpInResult result = authService.SignUp(new SignUpRequest() { });
                    
                    Assert.NotNull(result);
                    Assert.False(result.Ok);
                    Assert.Equal("Model is not valid", result.Info);
                    Assert.Null(result.User);
                    Assert.Null(result.AuthToken);
                    Assert.NotNull(result.Errors);
                    Assert.True(result.Errors.Any());
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void IsNotValidKeySignUp()
        {
            var authService = _server.Host.Services.GetService(typeof(IAuthService)) as IAuthService;
            var inviteService = _server.Host.Services.GetService(typeof(IInviteService)) as IInviteService;

            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<PlayCatDbContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new PlayCatDbContext(options))
                {
                    context.Database.EnsureCreated();
                }

                // Run the test against one instance of the context
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
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void IsValidModelSignUp()
        {
            var authService = _server.Host.Services.GetService(typeof(IAuthService)) as IAuthService;
            var inviteService = _server.Host.Services.GetService(typeof(IInviteService)) as IInviteService;

            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<PlayCatDbContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new PlayCatDbContext(options))
                {
                    context.Database.EnsureCreated();
                }

                // Run the test against one instance of the context
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
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void IsUsedEmailSignUp()
        {
            var authService = _server.Host.Services.GetService(typeof(IAuthService)) as IAuthService;
            var inviteService = _server.Host.Services.GetService(typeof(IInviteService)) as IInviteService;

            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<PlayCatDbContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new PlayCatDbContext(options))
                {
                    context.Database.EnsureCreated();
                }

                // Run the test against one instance of the context
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
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void IsUsedInviteSignUp()
        {
            var authService = _server.Host.Services.GetService(typeof(IAuthService)) as IAuthService;
            var inviteService = _server.Host.Services.GetService(typeof(IInviteService)) as IInviteService;

            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<PlayCatDbContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new PlayCatDbContext(options))
                {
                    context.Database.EnsureCreated();
                }

                // Run the test against one instance of the context
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
                }
            }
            finally
            {
                connection.Close();
            }
        }

        #endregion

        #region Sign In

        [Fact]
        public void IsValidModelSignIn()
        {
            var authService = _server.Host.Services.GetService(typeof(IAuthService)) as IAuthService;
            var inviteService = _server.Host.Services.GetService(typeof(IInviteService)) as IInviteService;

            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<PlayCatDbContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new PlayCatDbContext(options))
                {
                    context.Database.EnsureCreated();
                }

                // Run the test against one instance of the context
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
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void IsUserNotFoundSignIn()
        {
            var authService = _server.Host.Services.GetService(typeof(IAuthService)) as IAuthService;
            var inviteService = _server.Host.Services.GetService(typeof(IInviteService)) as IInviteService;

            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<PlayCatDbContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new PlayCatDbContext(options))
                {
                    context.Database.EnsureCreated();
                }

                // Run the test against one instance of the context
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
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void IsUserFoundButWrongPasswordSignIn()
        {
            var authService = _server.Host.Services.GetService(typeof(IAuthService)) as IAuthService;
            var inviteService = _server.Host.Services.GetService(typeof(IInviteService)) as IInviteService;

            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<PlayCatDbContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new PlayCatDbContext(options))
                {
                    context.Database.EnsureCreated();
                }

                // Run the test against one instance of the context
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
                }
            }
            finally
            {
                connection.Close();
            }
        }


        [Theory]
        [InlineData(null, null)]
        [InlineData(null, "12345abc")]
        [InlineData("mefgalm@gmail.com", null)]
        [InlineData("mefgalm@gmail.com", "12")]
        [InlineData("mefgalm@gmail", "123456abc")]
        [InlineData("mefgalm@gmail.com", "12345678")]
        [InlineData("mefgalm@gmail.com", "123456789ABCDEFGH")]
        [InlineData("mefgalm@gmail.com", "abcdertyuiop")]
        public void IsInvalidModelSignIn(string email, string password)
        {
            var authService = _server.Host.Services.GetService(typeof(IAuthService)) as IAuthService;
            var inviteService = _server.Host.Services.GetService(typeof(IInviteService)) as IInviteService;

            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<PlayCatDbContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new PlayCatDbContext(options))
                {
                    context.Database.EnsureCreated();
                }

                // Run the test against one instance of the context
                using (var context = new PlayCatDbContext(options))
                {
                    authService.SetDbContext(context);

                    SignUpInResult resultSignIn = authService.SignIn(new SignInRequest()
                    {
                        Email = email,
                        Password = password,
                    });

                    Assert.NotNull(resultSignIn);
                    Assert.False(resultSignIn.Ok);
                    Assert.Null(resultSignIn.User);
                    Assert.Null(resultSignIn.AuthToken);
                    Assert.NotNull(resultSignIn.Errors);
                    Assert.NotEmpty(resultSignIn.Errors);
                    Assert.Equal("Model is not valid", resultSignIn.Info);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        #endregion
    }
}
