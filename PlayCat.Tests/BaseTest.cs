using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PlayCat.DataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayCat.Tests
{
    public class BaseTest
    {
        protected readonly TestServer _server;

        public BaseTest()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<StartupTest>());
        }

        public void SqlLiteDatabaseTest(Action<DbContextOptions<PlayCatDbContext>> action)
        {
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

                action(options);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
