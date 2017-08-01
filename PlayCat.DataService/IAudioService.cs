using PlayCat.DataService.Request;
using PlayCat.DataService.Request.AudioRequest;
using PlayCat.DataService.Response;
using PlayCat.DataService.Response.AudioRequest;
using System;

namespace PlayCat.DataService
{
    public interface IAudioService : ISetDbContext
    {
        GetInfoResult GetInfo(UrlRequest request);
        BaseResult UploadAudio(Guid userId, UploadAudioRequest request);
    }
}