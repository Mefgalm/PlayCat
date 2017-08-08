using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayCat.DataService.Request
{
    public class UpdatePlaylistRequest : PlaylistRequest
    {
        public Guid PlaylistId { get; set; }
    }
}
