using Microsoft.AspNetCore.Mvc;
using PlayCat.DataService;
using PlayCat.DataService.Response.AudioResponse;
using PlayCat.DataService.Response.AuthResponse;
using System;

namespace PlayCat.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AudioController : BaseController
    {        
        private readonly IAudioService _audioService;
        private readonly IAuthService _authService;

        public AudioController(IAudioService audioService, IAuthService authService)
        {
            _audioService = audioService;
            _authService = authService;
        }

        [HttpGet("audios")]
        public AudioResult GetAudios(Guid playlistId, int skip, int take)
        {
            CheckTokenResult checkTokenResult = _authService.CheckToken(AccessToken);
            if (!checkTokenResult.Ok)
                return new AudioResult(checkTokenResult);

            return _audioService.GetAudios(playlistId, skip, take);
        }
    }
}