using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using PlayCat.DataService;

namespace PlayCat.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
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
                    context.Users.Add(new User()
                    {
                        Id = Guid.NewGuid(),
                        FirstName = "Vlad",
                    });
                    context.Users.Add(new User()
                    {
                        Id = Guid.NewGuid(),
                        FirstName = "not-Vlad",
                    });
                    context.SaveChanges();
                }

                // Use a separate instance of the context to verify correct data was saved to database
                //using (var context = new PlayCatDbContext(options))
                //{
                //    var service = new UserService(context);
                //    IEnumerable<User> users = service.ListAll();
                //    Assert.IsNotNull(users);
                //    Assert.AreEqual(2, users.Count());
                //}
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
