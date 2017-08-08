using Microsoft.AspNetCore.Mvc;
using PlayCat.DataService;
using PlayCat.DataService.Request;
using PlayCat.DataService.Response;
using System;

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

        [HttpDelete("delete/{playlistId}")]
        public BaseResult DeletePlaylist(Guid playlistId)
        {
            CheckTokenResult checkTokenResult = _authService.CheckToken(AccessToken);
            if (!checkTokenResult.Ok)
                return checkTokenResult;

            return _playlistService.DeletePlaylist(checkTokenResult.AuthToken.UserId, playlistId);
        }

        [HttpPut("update")]
        public PlaylistResult UpdatePlaylist([FromBody] UpdatePlaylistRequest request)
        {
            CheckTokenResult checkTokenResult = _authService.CheckToken(AccessToken);
            if (!checkTokenResult.Ok)
                return new PlaylistResult(checkTokenResult);

            return _playlistService.UpdatePlaylist(checkTokenResult.AuthToken.UserId, request);
        }

        [HttpPost("create")]
        public PlaylistResult CreatePlaylist([FromBody] PlaylistRequest request)
        {
            CheckTokenResult checkTokenResult = _authService.CheckToken(AccessToken);
            if (!checkTokenResult.Ok)
                return new PlaylistResult(checkTokenResult);

            return _playlistService.CreatePlaylist(checkTokenResult.AuthToken.UserId, request);
        }

        [HttpGet("userPlaylists")]
        public UserPlaylistsResult GetUserPlaylists()
        {
            CheckTokenResult checkTokenResult = _authService.CheckToken(AccessToken);
            if (!checkTokenResult.Ok)
                return new UserPlaylistsResult(checkTokenResult);

            return _playlistService.GetUserPlaylists(checkTokenResult.AuthToken.UserId);
        }

        [HttpGet("playlist")]
        public PlaylistResult GetPlaylist(Guid? playlist, int skip = 0, int take = 50)
        {
            CheckTokenResult checkTokenResult = _authService.CheckToken(AccessToken);
            if (!checkTokenResult.Ok)
                return new PlaylistResult(checkTokenResult);

            return _playlistService.GetPlaylist(checkTokenResult.AuthToken.UserId, playlist, skip, take);
        }
    }
}
