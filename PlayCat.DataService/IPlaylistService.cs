using System;
using PlayCat.DataService.Response.AudioRequest;

namespace PlayCat.DataService
{
    public interface IPlaylistService : ISetDbContext
    {
        PlaylistResult GetPlaylist(Guid userId, Guid? playlistId, int skip, int take);
        UserPlaylistsResult GetUserPlaylists(Guid userId);
    }
}