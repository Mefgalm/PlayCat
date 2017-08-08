using PlayCat.DataService.Request;
using PlayCat.DataService.Response;
using System;

namespace PlayCat.DataService
{
    public interface IAudioService : ISetDbContext
    {
        AudioResult GetAudios(Guid playlistId, int skip, int take);
        BaseResult RemoveFromPlaylist(Guid userId, AddRemovePlaylistRequest request);
        BaseResult AddToPlaylist(Guid userId, AddRemovePlaylistRequest request);
        AudioResult SearchAudios(string searchString, int skip, int take);
    }
}