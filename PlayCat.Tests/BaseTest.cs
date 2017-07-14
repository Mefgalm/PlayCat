using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
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
    }
}
