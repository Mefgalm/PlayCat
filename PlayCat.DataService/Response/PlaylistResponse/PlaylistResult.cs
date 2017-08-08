using PlayCat.ApiModel;
using System.Collections.Generic;

namespace PlayCat.DataService.Response
{
    public class PlaylistResult : BaseResult
    {
        public PlaylistResult() : base(new BaseResult())
        {
        }

        public PlaylistResult(BaseResult baseResult) : base(baseResult)
        {
        }

        public Playlist Playlist { get; set; }
    }
}
