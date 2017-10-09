using Microsoft.AspNetCore.Mvc;
using PlayCat.DataService;
using PlayCat.DataService.Request;
using PlayCat.DataService.Response;

namespace PlayCat.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("signUp")]
        public SignUpInResult SignUp([FromBody] SignUpRequest request)
        { 

            return _authService.SignUp(request);
        }

        [HttpPost("signIn")]
        public SignUpInResult SignIn([FromBody] SignInRequest request)
        {
            return _authService.SignIn(request);
        }
    }
}
