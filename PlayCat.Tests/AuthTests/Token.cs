using System;
using Xunit;
using PlayCat.DataService;

namespace PlayCat.Tests.AuthTests
{
    public class Token : BaseTest
    {
        [Fact]
        public void IsNotFound()
        {
            var authService = _server.Host.Services.GetService(typeof(IAuthService)) as IAuthService;

            var baseResult = authService.CheckToken(null);

            Assert.NotNull(baseResult);
            Assert.Equal("Token not found in headers", baseResult.Info);
            Assert.False(baseResult.ShowInfo);
            Assert.False(baseResult.Ok);
            Assert.Null(baseResult.Errors);
            Assert.Equal(ResponseCode.InvalidToken, baseResult.Code);
        }

        [Fact]
        public void IsWrongFormat()
        {
            var authService = _server.Host.Services.GetService(typeof(IAuthService)) as IAuthService;

            var baseResult = authService.CheckToken("123");

            Assert.NotNull(baseResult);
            Assert.Equal("Token wrong format", baseResult.Info);
            Assert.False(baseResult.ShowInfo);
            Assert.False(baseResult.Ok);
            Assert.Null(baseResult.Errors);
            Assert.Equal(ResponseCode.InvalidToken, baseResult.Code);
        }

        [Fact]
        public void IsNotFoundInDb()
        {
            SqlLiteDatabaseTest(options =>
            {
                var authService = _server.Host.Services.GetService(typeof(IAuthService)) as IAuthService;
                var inviteService = _server.Host.Services.GetService(typeof(IInviteService)) as IInviteService;

                using (var context = new PlayCatDbContext(options))
                {
                    authService.SetDbContext(context);

                    var baseResult = authService.CheckToken(Guid.Empty.ToString());

                    Assert.NotNull(baseResult);
                    Assert.Equal("Token not registered", baseResult.Info);
                    Assert.False(baseResult.ShowInfo);
                    Assert.False(baseResult.Ok);
                    Assert.Null(baseResult.Errors);
                    Assert.Equal(ResponseCode.InvalidToken, baseResult.Code);
                }
            });
        }

        [Fact]
        public void IsExpiredToken()
        {
            SqlLiteDatabaseTest(options =>
            {
                var authService = _server.Host.Services.GetService(typeof(IAuthService)) as IAuthService;
                var inviteService = _server.Host.Services.GetService(typeof(IInviteService)) as IInviteService;

                using (var context = new PlayCatDbContext(options))
                {
                    authService.SetDbContext(context);

                    var user = context.Users.Add(new DataModel.User()
                    {
                        Id = Guid.NewGuid(),
                        Email = "test@gmail.com",
                        FirstName = "test",
                        LastName = "test",
                        PasswordHash = "123123",
                        PasswordSalt = "123",
                        RegisterDate = DateTime.Now,
                        VerificationCode = "123",                        
                    });
                    
                    var authToken = context.AuthTokens.Add(new DataModel.AuthToken()
                    {
                        Id = Guid.NewGuid(),
                        DateExpired = DateTime.Now.AddDays(-1),
                        IsActive = true,
                        UserId = user.Entity.Id,
                    });

                    context.SaveChanges();

                    var baseResult = authService.CheckToken(authToken.Entity.Id.ToString());

                    Assert.NotNull(baseResult);
                    Assert.Equal("Token is expired", baseResult.Info);
                    Assert.False(baseResult.ShowInfo);
                    Assert.False(baseResult.Ok);
                    Assert.Null(baseResult.Errors);
                    Assert.Equal(ResponseCode.InvalidToken, baseResult.Code);
                }
            });
        }

        [Fact]
        public void IsTokenIsInactive()
        {
            SqlLiteDatabaseTest(options =>
            {
                var authService = _server.Host.Services.GetService(typeof(IAuthService)) as IAuthService;
                var inviteService = _server.Host.Services.GetService(typeof(IInviteService)) as IInviteService;

                using (var context = new PlayCatDbContext(options))
                {
                    authService.SetDbContext(context);

                    var user = context.Users.Add(new DataModel.User()
                    {
                        Id = Guid.NewGuid(),
                        Email = "test@gmail.com",
                        FirstName = "test",
                        LastName = "test",
                        PasswordHash = "123123",
                        PasswordSalt = "123",
                        RegisterDate = DateTime.Now,
                        VerificationCode = "123",
                    });

                    var authToken = context.AuthTokens.Add(new DataModel.AuthToken()
                    {
                        Id = Guid.NewGuid(),
                        DateExpired = DateTime.Now.AddDays(10),
                        IsActive = false,
                        UserId = user.Entity.Id,
                    });

                    context.SaveChanges();

                    var baseResult = authService.CheckToken(authToken.Entity.Id.ToString());

                    Assert.NotNull(baseResult);
                    Assert.Equal("Token is not active", baseResult.Info);
                    Assert.False(baseResult.ShowInfo);
                    Assert.False(baseResult.Ok);
                    Assert.Null(baseResult.Errors);
                    Assert.Equal(ResponseCode.InvalidToken, baseResult.Code);
                }
            });
        }

    }
}
