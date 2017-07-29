using PlayCat.DataService.Request;
using PlayCat.DataService.Response;

namespace PlayCat.DataService
{
    public interface IAudioService : ISetDbContext
    {
        GetInfoResult GetInfo(UrlRequest request);
        BaseResult UploadAudio(UploadAudioRequest request);
    }
}