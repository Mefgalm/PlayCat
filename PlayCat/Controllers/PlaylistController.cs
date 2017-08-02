using Microsoft.AspNetCore.Mvc;
using PlayCat.DataService;
using PlayCat.DataService.Response.AudioRequest;
using PlayCat.DataService.Response.AuthRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayCat.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PlaylistController : BaseController
    {
        private readonly IPlaylistService _playlistService;
        private readonly IAuthService _authService;

        public PlaylistController(IPlaylistService playlistService, IAuthService authService)
        {
            _playlistService = playlistService;
            _authService = authService;
        }

        [HttpGet("playlist")]
        public PlaylistResult UploadAudio(Guid? playlist, int skip = 0, int take = 50)
        {
            CheckTokenResult checkTokenResult = _authService.CheckToken(AccessToken);
            if (!checkTokenResult.Ok)
                return new PlaylistResult(checkTokenResult);

            return _playlistService.GetPlaylist(checkTokenResult.AuthToken.UserId, playlist, skip, take);
        }
    }
}
