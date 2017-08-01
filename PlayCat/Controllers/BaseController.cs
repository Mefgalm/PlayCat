using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayCat.Controllers
{
    public class BaseController : Controller
    {
        private const string AccessTokenKey = "AccessToken";

        public string AccessToken => HttpContext.Request.Headers[AccessTokenKey];
    }
}
