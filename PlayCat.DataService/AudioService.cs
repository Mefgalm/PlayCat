using System.Linq;
using System;
using PlayCat.DataService.DTO;
using PlayCat.DataService.Mappers;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using PlayCat.DataService.Response.AudioResponse;
using PlayCat.DataService.Response;

namespace PlayCat.DataService
{
    public class AudioService : BaseService, IAudioService
    {        
        public AudioService(PlayCatDbContext dbContext, 
            ILoggerFactory loggerFactory)
            : base(dbContext, loggerFactory.CreateLogger<AuthService>())
        {
        }                

        public BaseResult RemoveFromPlaylist(Guid userId, Guid playlistId, Guid audioId)
        {
            return RequestTemplate(() =>
            {
                //Is need check audioId??? - perfomance issue
                var playlistInfo =
                    (from p in _dbContext.Playlists
                     join ap in _dbContext.AudioPlaylists on p.Id equals ap.PlaylistId into _ap
                     where p.Id == playlistId && p.OwnerId == userId
                     select new
                     {
                         Playlist = p,
                         AddedAudioPlaylist = _ap.FirstOrDefault(x => x.AudioId == audioId),
                     })
                    .FirstOrDefault();

                if (playlistInfo == null)
                    return ResponseBuilder<BaseResult>.Create().Fail().SetInfoAndBuild("Playlist not found");

                if (playlistInfo.AddedAudioPlaylist != null)
                {
                    _dbContext.AudioPlaylists.Remove(playlistInfo.AddedAudioPlaylist);
                    _dbContext.SaveChanges();
                }                    

                return ResponseBuilder<BaseResult>.SuccessBuild();
            });
        }

        public BaseResult AddToPlaylist(Guid userId, Guid playlistId, Guid audioId)
        {
            return RequestTemplate(() =>
            {
                //Is need check audioId??? - perfomance issue
                var playlistInfo =
                    (from p in _dbContext.Playlists
                    join ap in _dbContext.AudioPlaylists on p.Id equals ap.PlaylistId into _ap
                    where p.Id == playlistId && p.OwnerId == userId
                    select new
                    {
                        Playlist = p,
                        IsAlreadyAdded = _ap.Any(x => x.AudioId == audioId),
                    })
                    .FirstOrDefault();

                if (playlistInfo == null)
                    return ResponseBuilder<BaseResult>.Create().Fail().SetInfoAndBuild("Playlist not found");

                if (playlistInfo.IsAlreadyAdded)
                    return ResponseBuilder<BaseResult>.Create().Fail().SetInfoAndBuild("Audio is already added");

                var audioPlaylist = new DataModel.AudioPlaylist()
                {
                    AudioId = audioId,
                    DateCreated = DateTime.Now,
                    Order = playlistInfo.Playlist.OrderValue,
                    PlaylistId = playlistId,                    
                };

                _dbContext.AudioPlaylists.Add(audioPlaylist);
                _dbContext.SaveChanges();

                return ResponseBuilder<BaseResult>.SuccessBuild();
            });
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
                     orderby ap.Order descending
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
