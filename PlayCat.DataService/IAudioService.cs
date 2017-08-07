using PlayCat.DataService.Request;
using PlayCat.DataService.Request.AudioRequest;
using PlayCat.DataService.Response;
using PlayCat.DataService.Response.AudioResponse;
using System;

namespace PlayCat.DataService
{
    public interface IAudioService : ISetDbContext
    {
        AudioResult GetAudios(Guid playlistId, int skip, int take);
    }
}