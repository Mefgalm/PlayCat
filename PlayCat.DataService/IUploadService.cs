using System;
using PlayCat.DataService.Request;
using PlayCat.DataService.Response;

namespace PlayCat.DataService
{
    public interface IUploadService : ISetDbContext
    {
        GetInfoResult GetInfo(UrlRequest request);
        BaseResult UploadAudio(Guid userId, UploadAudioRequest request);
    }
}