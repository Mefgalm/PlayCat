using PlayCat.DataService.DTO;
using System.Collections.Generic;
using System.Linq;

namespace PlayCat.DataService.Mappers
{
    public static class PlaylistMapper
    {
        public static class ToApi
        {
            public static ApiModel.Playlist FromDTO(PlaylistDTO playlistDTO)
            {
                return playlistDTO == null ? null : new ApiModel.Playlist()
                {
                    Id = playlistDTO.Id,
                    Title = playlistDTO.Title,
                    IsGeneral = playlistDTO.IsGeneral,
                    Owner = UserMapper.ToApi.FromData(playlistDTO.Owner),
                    Audios = playlistDTO.Audios.Select(x => AudioMapper.ToApi.FromDTO(x)),
                };
            }

            public static ApiModel.Playlist FromData(DataModel.Playlist playlist)
            {
                return playlist == null ? null : new ApiModel.Playlist()
                {
                    Id = playlist.Id,
                    IsGeneral = playlist.IsGeneral,
                    Title = playlist.Title,
                    Owner = UserMapper.ToApi.FromData(playlist.Owner),
                };
            }
        }
    }
}
