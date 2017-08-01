using PlayCat.DataService.Request;
using PlayCat.DataService.Response;
using System;

namespace PlayCat.DataService
{
    public interface IAudioService : ISetDbContext
    {
        GetInfoResult GetInfo(UrlRequest request);
        BaseResult UploadAudio(Guid userId, UploadAudioRequest request);
    }
}