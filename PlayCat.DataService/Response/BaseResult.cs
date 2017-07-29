using System.Collections.Generic;

namespace PlayCat.DataService.Response
{
    public class BaseResult
    {
        public bool Ok { get; set; } = true;
        public bool ShowInfo { get; set; } = true;
        public string Info { get; set; }
        public IDictionary<string, string> Errors { get; set; }
        public ResponseCode Code { get; set; } = ResponseCode.None;
    }
}
