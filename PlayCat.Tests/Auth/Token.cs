using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Http.Features;
using System.Security.Claims;
using System.Threading;
using System.IO;
using PlayCat.DataService;
using PlayCat.DataService.Request;
using PlayCat.DataService.Response;
using Microsoft.EntityFrameworkCore;

namespace PlayCat.Tests.Auth
{
    public class Token : BaseTest
    {
        [Fact]
        public void IsNotFound()
        {
            var authService = _server.Host.Services.GetService(typeof(IAuthService)) as IAuthService;

            var baseResult = authService.CheckToken(null);

            Assert.NotNull(baseResult);
            Assert.Equal("Token not found", baseResult.Info);
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
                    Assert.Equal("Token not found", baseResult.Info);
                    Assert.False(baseResult.ShowInfo);
                    Assert.False(baseResult.Ok);
                    Assert.Null(baseResult.Errors);
                    Assert.Equal(ResponseCode.InvalidToken, baseResult.Code);
                }
            });
        }
    }
}
