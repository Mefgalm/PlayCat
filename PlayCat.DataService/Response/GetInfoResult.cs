using PlayCat.Music;
using System.Collections.Generic;

namespace PlayCat.DataService.Response
{
    public class GetInfoResult : BaseResult
    {
        public IUrlInfo UrlInfo { get; set; }        
    }
}
