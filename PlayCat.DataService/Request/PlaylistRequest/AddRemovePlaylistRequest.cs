using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayCat.DataService.Request
{
    public class AddRemovePlaylistRequest
    {
        public Guid PlaylistId { get; set; }

        public Guid AudioId { get; set; }
    }
}
