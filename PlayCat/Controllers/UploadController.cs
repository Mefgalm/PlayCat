using Microsoft.AspNetCore.Mvc;
using PlayCat.DataService;
using PlayCat.DataService.Request;
using PlayCat.DataService.Response;

namespace PlayCat.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UploadController : BaseController
    {
        private readonly IUploadService _uploadService;
        private readonly IAuthService _authService;

        public UploadController(IUploadService uploadService, IAuthService authService)
        {
            _uploadService = uploadService;
            _authService = authService;
        }

        [HttpPost("videoInfo")]
        public GetInfoResult GetUrlInfo([FromBody] UrlRequest request)
        {
            CheckTokenResult checkTokenResult = _authService.CheckToken(AccessToken);
            if (!checkTokenResult.Ok)
                return new GetInfoResult(checkTokenResult);

            return _uploadService.GetInfo(request);
        }

        [HttpPost("uploadAudio")]
        public BaseResult UploadAudio([FromBody] UploadAudioRequest request)
        {
            CheckTokenResult checkTokenResult = _authService.CheckToken(AccessToken);
            if (!checkTokenResult.Ok)
                return checkTokenResult;

            return _uploadService.UploadAudio(checkTokenResult.AuthToken.UserId, request);
        }
    }
}
