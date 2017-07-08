using PlayCat.ApiModel;
using System.Collections.Generic;

namespace PlayCat.DataService.Response
{
    public class SignUpResult : BaseResult
    {
        public User User { get; set; }

        public AuthToken AuthToken { get; set; }

        public IDictionary<string, string> Errors { get; set; }
    }
}
