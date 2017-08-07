using PlayCat.DataService.Request.AuthRequest;
using PlayCat.DataService.Response.AuthResponse;

namespace PlayCat.DataService
{
    public interface IAuthService : ISetDbContext
    {
        CheckTokenResult CheckToken(string token);
        SignUpInResult SignIn(SignInRequest request);
        SignUpInResult SignUp(SignUpRequest request);
    }
}