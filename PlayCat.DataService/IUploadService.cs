using System;
using PlayCat.DataService.Request;
using PlayCat.DataService.Response;
using PlayCat.DataService.Response.UploadResponse;

namespace PlayCat.DataService
{
    public interface IUploadService : ISetDbContext
    {
        GetInfoResult GetInfo(UrlRequest request);
        UploadResult UploadAudio(Guid userId, UploadAudioRequest request);
    }
}