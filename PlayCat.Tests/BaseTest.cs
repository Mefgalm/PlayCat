using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PlayCat.DataService;
using PlayCat.DataService.Response;
using System;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayCat.Tests.Extensions;

namespace PlayCat.Tests
{
    public class BaseTest
    {
        protected const string BaseAudioExtension = ".mp3";

        protected readonly TestServer _server;

        public BaseTest()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<StartupTest>());
        }

        public void CheckIfSuccess(BaseResult result)
        {
            Assert.NotNull(result);
            Assert.True(result.Ok);
            Assert.Null(result.Errors);
        }

        public void CheckIfFail(BaseResult result)
        {
            Assert.NotNull(result);
            Assert.False(result.Ok);
            Assert.NotNull(result.Info);
            Assert.True(result.Info.Length > 0);
        }


        protected Guid GetUserId(PlayCatDbContext context)
        {
            var inviteService = _server.Host.Services.GetService(typeof(IInviteService)) as IInviteService;

            string password = "123456abc";
            string email = "test@gmail.com";

            DataModel.User user = context.CreateUser(email, "test", "test", password, inviteService.GenerateInvite());
            DataModel.AuthToken authToken = context.CreateToken(DateTime.Now.AddDays(-1), false, user.Id);

            context.SaveChanges();

            return user.Id;
        }

        protected void SqlLiteDatabaseTest(Action<DbContextOptions<PlayCatDbContext>> action)
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<PlayCatDbContext>()
                    .UseSqlite(connection)
                    .Options;

                using (var context = new PlayCatDbContext(options))
                {
                    context.Database.EnsureCreated();
                }

                action(options);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
