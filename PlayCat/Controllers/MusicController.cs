using Microsoft.AspNetCore.Mvc;
using PlayCat.DataService;
using PlayCat.Music;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PlayCat.Controllers
{
    [Route("api/[controller]")]
    public class MusicController : BaseController
    {
        private readonly IFileResolver _fileResolver;
        private readonly IAuthService _authService;

        public MusicController(IFileResolver fileResolver, IAuthService authService)
        {
            _authService = authService;
            _fileResolver = fileResolver;
        }

        [HttpGet("song/{filename}/storageType/fileSytem")]
        public FileResult Song(string filename)
        {
            //var result = _authService.CheckToken(AccessToken);

            string contentType = "audio/mpeg";

            string audioFolderPath = _fileResolver.GetAudioFolderPath(StorageType.FileSystem);

            HttpContext.Response.ContentType = contentType;
            FileContentResult result = new FileContentResult(
                System.IO.File.ReadAllBytes(Path.Combine(audioFolderPath, filename)),
                contentType);

            return result;
        }
    }
}
