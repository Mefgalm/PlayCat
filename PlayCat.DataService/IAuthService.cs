using PlayCat.DataService.Request;
using PlayCat.DataService.Response;

namespace PlayCat.DataService
{
    public interface IAuthService
    {
        void SignIn();
        SignUpResult SignUp(SignUpRequest request);

        void SetDbContext(PlayCatDbContext dbContext);
    }
}