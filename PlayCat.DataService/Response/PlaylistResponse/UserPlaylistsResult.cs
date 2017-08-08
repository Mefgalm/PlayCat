using PlayCat.ApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayCat.DataService.Response
{
    public class UserPlaylistsResult : BaseResult
    {
        public UserPlaylistsResult() : base(new BaseResult())
        {
        }

        public UserPlaylistsResult(BaseResult baseResult) : base(baseResult)
        {
        }

        public IEnumerable<Playlist> Playlists { get; set; } = Enumerable.Empty<Playlist>();
    }
}
