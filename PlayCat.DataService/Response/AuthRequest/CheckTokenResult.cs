using PlayCat.DataModel;

namespace PlayCat.DataService.Response.AuthRequest
{
    public class CheckTokenResult : BaseResult
    {
        public AuthToken AuthToken { get; set; }
    }
}
