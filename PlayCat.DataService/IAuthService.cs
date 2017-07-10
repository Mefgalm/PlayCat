using PlayCat.DataService.Request;
using PlayCat.DataService.Response;

namespace PlayCat.DataService
{
    public interface IAuthService : ISetDbContext
    {
        SignUpInResult SignIn(SignInRequest request);
        SignUpInResult SignUp(SignUpRequest request);
    }
}