using Microsoft.AspNetCore.Mvc;
using PlayCat.DataService;
using PlayCat.DataService.Response;

namespace PlayCat.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AudioController : Controller
    {
        private readonly IAudioService _audioService;

        public AudioController(IAudioService audioService)
        {
            _audioService = audioService;
        }

        [HttpGet("uploadAudio")]
        public UploadAudioResult UploadAudio(string youtubeUrl)
        {
            return _audioService.UploadAudio(youtubeUrl);
        }
    }
}