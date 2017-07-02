using Microsoft.AspNetCore.Mvc;
using PlayCat.DataService;
using PlayCat.DataService.Response;

namespace PlayCat.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        public AuthController(AudioService audioService)
        {
            
        }

        [HttpGet("uploadAudio")]
        public UploadAudioResult UploadAudio(string youtubeUrl)
        {
            return null;
        }
    }
}
