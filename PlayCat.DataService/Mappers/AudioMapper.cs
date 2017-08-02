using PlayCat.DataService.DTO;
using System;

namespace PlayCat.DataService.Mappers
{
    public static class AudioMapper
    {
        public static class ToApi
        {
            public static ApiModel.Audio FromDTO(AudioDTO audioDTO)
            {
                return audioDTO == null ? null : new ApiModel.Audio
                {
                    AccessUrl = audioDTO.AccessUrl,
                    Artist = audioDTO.Artist,
                    DateAdded = audioDTO.DateAdded,
                    Id = audioDTO.Id,
                    Song = audioDTO.Song,
                    Uploader = UserMapper.ToApi.FromData(audioDTO.Uploader),                    
                };
            }
        }
    }
}
