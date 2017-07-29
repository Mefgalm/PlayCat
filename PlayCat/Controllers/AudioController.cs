using Microsoft.AspNetCore.Mvc;
using PlayCat.DataService;
using PlayCat.DataService.Request;
using PlayCat.DataService.Response;

namespace PlayCat.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AudioController : Controller
    {
        private const string AccessToken = "AccessToken";

        private readonly IAudioService _audioService;
        private readonly IAuthService _authService;

        public AudioController(IAudioService audioService, IAuthService authService)
        {
            _audioService = audioService;
            _authService = authService;
        }

        [HttpPost("videoInfo")]
        public GetInfoResult GetUrlInfo(UrlRequest request)
        {
            return _audioService.GetInfo(request);
        }

        [HttpPost("uploadAudio")]
        public BaseResult UploadAudio(UploadAudioRequest request)
        {
            return _audioService.UploadAudio(request);
        }
    }
}