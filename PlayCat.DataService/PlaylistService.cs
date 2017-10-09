using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PlayCat.DataService.Response;
using PlayCat.DataService.Mappers;
using PlayCat.DataService.DTO;
using PlayCat.DataService.Request;

namespace PlayCat.DataService
{
    public class PlaylistService : BaseService, IPlaylistService
    {
        private const string PlaylistNotFound = "Playlist not found";

        public PlaylistService(PlayCatDbContext dbContext, ILoggerFactory loggerFactory) : base(dbContext, loggerFactory.CreateLogger<PlaylistService>())
        {
        }

        public PlaylistResult UpdatePlaylist(Guid userId, UpdatePlaylistRequest request)
        {
            return BaseInvokeCheckModel(request, () =>
            {
                DataModel.Playlist playlist = _dbContext.Playlists.FirstOrDefault(x => x.Id == request.PlaylistId && x.OwnerId == userId);

                if (playlist == null)
                    return ResponseBuilder<PlaylistResult>.Fail().SetInfoAndBuild(PlaylistNotFound);

                playlist.Title = request.Title;

                _dbContext.SaveChanges();

                return ResponseBuilder<PlaylistResult>.SuccessBuild(new PlaylistResult()
                {
                    Playlist = PlaylistMapper.ToApi.FromData(playlist),
                });
            });
        }

        public BaseResult DeletePlaylist(Guid userId, Guid playlistId)
        {
            return BaseInvoke(() =>
            {
                var playlistWithAudios =
                    (from p in _dbContext.Playlists
                     join ap in _dbContext.AudioPlaylists on p.Id equals ap.PlaylistId into _ap
                     where p.Id == playlistId && p.OwnerId == userId
                     select new
                     {
                         Playlist = p,
                         AudioPlaylists = _ap,
                     })
                    .FirstOrDefault();

                if (playlistWithAudios == null)
                    return ResponseBuilder<BaseResult>.Fail().SetInfoAndBuild(PlaylistNotFound);

                if (playlistWithAudios.Playlist.IsGeneral)
                    return ResponseBuilder<BaseResult>.Fail().SetInfoAndBuild("General playlist cannot be removed");

                _dbContext.Playlists.Remove(playlistWithAudios.Playlist);
                _dbContext.AudioPlaylists.RemoveRange(playlistWithAudios.AudioPlaylists);
                _dbContext.SaveChanges();

                return ResponseBuilder<BaseResult>.SuccessBuild();
            });
        }

        public PlaylistResult CreatePlaylist(Guid userId, CreatePlaylistRequest request)
        {
            return BaseInvokeCheckModel(request, () =>
            {
                var playlist = new DataModel.Playlist()
                {
                    Id = Guid.NewGuid(),
                    IsGeneral = false,
                    OrderValue = 0,
                    OwnerId = userId,
                    Title = request.Title,
                };

                _dbContext.Playlists.Add(playlist);
                _dbContext.SaveChanges();

                return ResponseBuilder<PlaylistResult>.SuccessBuild(new PlaylistResult()
                {
                    Playlist = PlaylistMapper.ToApi.FromData(playlist),
                });
            });
        }

        public UserPlaylistsResult GetUserPlaylists(Guid userId, Guid? playlistId, int skip, int take)
        {
            return BaseInvoke(() =>
            {
                //get audios with paging
                var playlistAudiosQry =
                    (from ap in _dbContext.AudioPlaylists
                     join a in _dbContext.Audios on ap.AudioId equals a.Id
                     orderby ap.Order descending
                     select new
                     {
                         PlaylistId = ap.PlaylistId,
                         AudioDTO = new AudioDTO()
                         {
                             Id = a.Id,
                             AccessUrl = a.AccessUrl,
                             Artist = a.Artist,
                             Duration = a.Duration,
                             DateAdded = ap.DateCreated,
                             Song = a.Song,
                             Uploader = a.Uploader,
                         },
                     })
                    .Skip(skip)
                    .Take(take);

                IQueryable<PlaylistDTO> playlistDtoQry = null;
                if (playlistId.HasValue)
                {
                    //join playlist to with audios
                    playlistDtoQry =
                        (from p in _dbContext.Playlists
                         join paq in playlistAudiosQry.Where(x => x.PlaylistId == playlistId) on p.Id equals paq.PlaylistId into _paq
                         where p.OwnerId == userId
                         select new PlaylistDTO()
                         {
                             Id = p.Id,
                             IsGeneral = p.IsGeneral,
                             Owner = p.Owner,
                             Title = p.Title,
                             Audios = _paq.Select(x => x.AudioDTO),
                         });
                }
                else
                {
                    //if no playlist id then select from general
                    playlistDtoQry =
                        (from p in _dbContext.Playlists
                         join paq in playlistAudiosQry on new { playlistId = p.Id, isGeneral = p.IsGeneral }
                                                   equals new { playlistId = paq.PlaylistId, isGeneral = true } into _paq
                         where p.OwnerId == userId
                         select new PlaylistDTO()
                         {
                             Id = p.Id,
                             IsGeneral = p.IsGeneral,
                             Owner = p.Owner,
                             Title = p.Title,
                             Audios = _paq.Select(x => x.AudioDTO),
                         });
                }

                IEnumerable<ApiModel.Playlist> apiPlaylists =
                    playlistDtoQry
                                .OrderByDescending(x => x.IsGeneral)
                                .ThenBy(x => x.Title)
                                .ToList()
                                .Select(x => PlaylistMapper.ToApi.FromDTO(x));

                return ResponseBuilder<UserPlaylistsResult>.SuccessBuild(new UserPlaylistsResult()
                {
                    Playlists = apiPlaylists,
                });
            });
        }
    }
}
