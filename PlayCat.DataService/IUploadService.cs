using System;
using PlayCat.DataService.Request.AudioRequest;
using PlayCat.DataService.Response;
using PlayCat.DataService.Response.AudioResponse;

namespace PlayCat.DataService
{
    public interface IUploadService : ISetDbContext
    {
        GetInfoResult GetInfo(UrlRequest request);
        BaseResult UploadAudio(Guid userId, UploadAudioRequest request);
    }
}