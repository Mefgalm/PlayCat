using PlayCat.DataModel;

namespace PlayCat.DataService.Response
{
    public class CheckTokenResult : BaseResult
    {
        public AuthToken AuthToken { get; set; }
    }
}
