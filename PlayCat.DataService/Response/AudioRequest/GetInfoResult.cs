using PlayCat.Music;

namespace PlayCat.DataService.Response.AudioRequest
{
    public class GetInfoResult : BaseResult
    {
        public GetInfoResult() : base(new BaseResult())
        {
        }

        public GetInfoResult(BaseResult baseResult) : base(baseResult)
        {
        }

        public IUrlInfo UrlInfo { get; set; }        
    }
}
