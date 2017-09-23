using System;
using PlayCat.DataService.Request;
using PlayCat.DataService.Response;
using PlayCat.DataService.Response.UploadResponse;
using System.Threading.Tasks;

namespace PlayCat.DataService
{
    public interface IUploadService : ISetDbContext
    {
        GetInfoResult GetInfo(UrlRequest request);
        Task<UploadResult> UploadAudioAsync(Guid userId, UploadAudioRequest request);
    }
}