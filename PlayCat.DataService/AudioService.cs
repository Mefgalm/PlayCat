using System.Linq;
using System;
using PlayCat.DataService.Response.AudioRequest;
using PlayCat.DataService.DTO;
using PlayCat.DataService.Mappers;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace PlayCat.DataService
{
    public class AudioService : BaseService, IAudioService
    {        
        public AudioService(PlayCatDbContext dbContext, 
            ILoggerFactory loggerFactory)
            : base(dbContext, loggerFactory.CreateLogger<AuthService>())
        {
        }                

        public AudioResult GetAudios(Guid playlistId, int skip, int take)
        {
            return RequestTemplate(() =>
            {
                //get audios with paging
                IEnumerable<ApiModel.Audio> apiAudios =
                    (from ap in _dbContext.AudioPlaylists
                     join a in _dbContext.Audios on ap.AudioId equals a.Id
                     where ap.PlaylistId == playlistId
                     orderby ap.DateCreated descending
                     select new AudioDTO()
                     {
                         Id = a.Id,
                         AccessUrl = a.AccessUrl,
                         Artist = a.Artist,
                         DateAdded = ap.DateCreated,
                         Song = a.Song,
                         Uploader = a.Uploader,
                     })
                    .Skip(skip)
                    .Take(take)
                    .ToList()
                    .Select(x => AudioMapper.ToApi.FromDTO(x));

                return ResponseBuilder<AudioResult>.SuccessBuild(new AudioResult()
                {
                    Audios = apiAudios,
                });
            });
        }                
    }
}
