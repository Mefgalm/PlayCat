using Microsoft.AspNetCore.Http;
using PlayCat.DataService.Request;
using PlayCat.DataService.Request.AuthRequest;
using PlayCat.DataService.Response;
using PlayCat.DataService.Response.AuthRequest;

namespace PlayCat.DataService
{
    public interface IAuthService : ISetDbContext
    {
        CheckTokenResult CheckToken(string token);
        SignUpInResult SignIn(SignInRequest request);
        SignUpInResult SignUp(SignUpRequest request);
    }
}