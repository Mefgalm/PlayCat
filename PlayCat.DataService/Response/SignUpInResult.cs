using PlayCat.ApiModel;
using System.Collections.Generic;

namespace PlayCat.DataService.Response
{
    public class SignUpInResult : BaseResult
    {
        public User User { get; set; }

        public AuthToken AuthToken { get; set; }

        public IDictionary<string, IEnumerable<string>> Errors { get; set; }
    }
}
