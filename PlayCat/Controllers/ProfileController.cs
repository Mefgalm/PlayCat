using Microsoft.AspNetCore.Mvc;
using PlayCat.DataService;
using PlayCat.DataService.Request;
using PlayCat.DataService.Response;
using System;

namespace PlayCat.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ProfileController : BaseController
    {
        private readonly IProfileService _profileService;
        private readonly IAuthService _authService;
    
        public ProfileController(IProfileService profileService, IAuthService authService)
        {
            _profileService = profileService;
            _authService = authService;
        }

        [HttpGet]
        public GetUpdateProfileResult GetProfile(Guid id)
        {
            CheckTokenResult checkTokenResult = _authService.CheckToken(AccessToken);
            if (!checkTokenResult.Ok)
                return new GetUpdateProfileResult(checkTokenResult);

            return _profileService.GetProfile(id);
        }

        [HttpPut]
        public GetUpdateProfileResult UpdateProfile(UpdateProfileRequest request)
        {
            CheckTokenResult checkTokenResult = _authService.CheckToken(AccessToken);
            if (!checkTokenResult.Ok)
                return new GetUpdateProfileResult(checkTokenResult);

            return _profileService.UpdateProfile(request);
        }
    }
}
