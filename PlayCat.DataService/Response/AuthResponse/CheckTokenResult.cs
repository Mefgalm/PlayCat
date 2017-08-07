using PlayCat.DataModel;

namespace PlayCat.DataService.Response.AuthResponse
{
    public class CheckTokenResult : BaseResult
    {
        public AuthToken AuthToken { get; set; }
    }
}
