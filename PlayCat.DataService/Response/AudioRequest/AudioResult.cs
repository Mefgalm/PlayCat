using PlayCat.ApiModel;
using System.Collections.Generic;
using System.Linq;

namespace PlayCat.DataService.Response.AudioRequest
{
    public class AudioResult : BaseResult
    {
        public AudioResult() : base(new BaseResult())
        {
        }

        public AudioResult(BaseResult baseResult) : base(baseResult)
        {
        }

        public IEnumerable<Audio> Audios { get; set; } = Enumerable.Empty<Audio>();
    }
}
