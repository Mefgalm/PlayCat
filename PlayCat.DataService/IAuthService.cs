using Microsoft.AspNetCore.Http;
using PlayCat.DataService.Request;
using PlayCat.DataService.Response;

namespace PlayCat.DataService
{
    public interface IAuthService : ISetDbContext
    {
        CheckTokenResult CheckToken(string token);
        SignUpInResult SignIn(SignInRequest request);
        SignUpInResult SignUp(SignUpRequest request);
    }
}