using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlayCat.DataService;
using PlayCat.DataService.ReturnTypes;

namespace PlayCat.Controllers
{
    [Produces("application/json")]
    [Route("api/audio")]
    public class AudioController : Controller
    {
        private readonly AudioService _audioService;

        public AudioController(AudioService audioService)
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