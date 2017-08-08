using System;
using PlayCat.DataService.Request;
using PlayCat.DataService.Response;

namespace PlayCat.DataService
{
    public interface IPlaylistService : ISetDbContext
    {
        PlaylistResult CreatePlaylist(Guid userId, PlaylistRequest request);
        BaseResult DeletePlaylist(Guid userId, Guid playlistId);
        PlaylistResult GetPlaylist(Guid userId, Guid? playlistId, int skip, int take);
        UserPlaylistsResult GetUserPlaylists(Guid userId);
        PlaylistResult UpdatePlaylist(Guid userId, UpdatePlaylistRequest request);
    }
}