using Microsoft.AspNetCore.Mvc;
using PlayCat.DataService;
using PlayCat.DataService.Request;
using PlayCat.DataService.Request.AudioRequest;
using PlayCat.DataService.Response;
using PlayCat.DataService.Response.AudioRequest;
using PlayCat.DataService.Response.AuthRequest;
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

        [HttpPost("videoInfo")]
        public GetInfoResult GetUrlInfo([FromBody] UrlRequest request)
        {
            CheckTokenResult checkTokenResult = _authService.CheckToken(AccessToken);
            if (!checkTokenResult.Ok)
                return new GetInfoResult(checkTokenResult);

            return _audioService.GetInfo(request);
        }        

        [HttpPost("uploadAudio")]
        public BaseResult UploadAudio([FromBody] UploadAudioRequest request)
        {
            CheckTokenResult checkTokenResult = _authService.CheckToken(AccessToken);
            if (!checkTokenResult.Ok)
                return checkTokenResult;

            return _audioService.UploadAudio(checkTokenResult.AuthToken.UserId, request);
        }
    }
}