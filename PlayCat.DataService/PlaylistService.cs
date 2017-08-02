using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PlayCat.DataService.Response.AudioRequest;
using PlayCat.DataService.Mappers;
using PlayCat.DataService.DTO;

namespace PlayCat.DataService
{
    public class PlaylistService : BaseService, IPlaylistService
    {
        public PlaylistService(PlayCatDbContext dbContext, ILoggerFactory loggerFactory) : base(dbContext, loggerFactory.CreateLogger<PlaylistService>())
        {
        }

        public UserPlaylistsResult GetUserPlaylists(Guid userId)
        {
            return RequestTemplate(() =>
            {
                IEnumerable<ApiModel.Playlist> apiPlaylists =
                    (from p in _dbContext.Playlists
                     where p.OwnerId == userId
                     orderby p.Title
                     select p)
                    .ToList()
                    .Select(x => PlaylistMapper.ToApi.FromData(x));

                return ResponseBuilder<UserPlaylistsResult>.SuccessBuild(new UserPlaylistsResult()
                {
                    Playlists = apiPlaylists,
                });
            });
        }

        public PlaylistResult GetPlaylist(Guid userId, Guid? playlistId, int skip, int take)
        {
            return RequestTemplate(() =>
            {
                //get audios with paging
                var playlistAudiosQry =
                    (from ap in _dbContext.AudioPlaylists
                     join a in _dbContext.Audios on ap.AudioId equals a.Id
                     orderby ap.DateCreated descending
                     select new
                     {
                         PlaylistId = ap.PlaylistId,
                         AudioDTO = new AudioDTO()
                         {
                             Id = a.Id,
                             AccessUrl = a.AccessUrl,
                             Artist = a.Artist,
                             DateAdded = ap.DateCreated,
                             Song = a.Song,
                             Uploader = a.Uploader,
                         },
                     })
                    .Skip(skip)
                    .Take(take);

                //join playlist to with audios
                var playlistWithAudiosQry =
                    from p in _dbContext.Playlists
                    join paq in playlistAudiosQry on p.Id equals paq.PlaylistId into _paq
                    select new PlaylistDTO()
                    {
                        Id = p.Id,
                        IsGeneral = p.IsGeneral,
                        Owner = p.Owner,
                        Title = p.Title,
                        Audios = _paq.Select(x => x.AudioDTO),
                    };

                //select playlist and audios base on playlistId or take General playlist
                if (playlistId.HasValue)
                {
                    playlistWithAudiosQry = playlistWithAudiosQry.Where(x => x.Id == playlistId.Value);
                }
                else
                {
                    playlistWithAudiosQry = playlistWithAudiosQry.Where(x => x.Owner.Id == userId && x.IsGeneral);
                }

                PlaylistDTO playlistDTO = playlistWithAudiosQry.FirstOrDefault();

                if (playlistDTO == null)
                    return ResponseBuilder<PlaylistResult>.Create().Fail().SetInfoAndBuild("Playlist not found");

                return ResponseBuilder<PlaylistResult>.SuccessBuild(new PlaylistResult()
                {
                    Playlist = PlaylistMapper.ToApi.FromDTO(playlistDTO),
                });
            });
        }
    }
}
