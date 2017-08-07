using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PlayCat.DataService.Response.AudioResponse;
using PlayCat.DataService.Mappers;
using PlayCat.DataService.DTO;
using PlayCat.DataService.Response.PlaylistResponse;
using PlayCat.DataService.Request.PlaylistRequest;
using PlayCat.DataService.Response;

namespace PlayCat.DataService
{
    public class PlaylistService : BaseService, IPlaylistService
    {
        private const string PlaylistNotFound = "Playlist not found";

        public PlaylistService(PlayCatDbContext dbContext, ILoggerFactory loggerFactory) : base(dbContext, loggerFactory.CreateLogger<PlaylistService>())
        {
        }

        public PlaylistResult UpdatePlaylist(Guid userId, Guid playlistId, PlaylistRequest request)
        {
            return RequestTemplateCheckModel(request, () =>
            {
                DataModel.Playlist playlist = _dbContext.Playlists.FirstOrDefault(x => x.Id == playlistId && x.OwnerId == userId);

                if (playlist == null)
                    return ResponseBuilder<PlaylistResult>.Create().Fail().SetInfoAndBuild(PlaylistNotFound);

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
            return RequestTemplate(() =>
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
                    return ResponseBuilder<BaseResult>.Create().Fail().SetInfoAndBuild(PlaylistNotFound);

                if(playlistWithAudios.Playlist.IsGeneral)
                    return ResponseBuilder<BaseResult>.Create().Fail().SetInfoAndBuild("General playlist cannot be removed");

                _dbContext.Playlists.Remove(playlistWithAudios.Playlist);
                _dbContext.AudioPlaylists.RemoveRange(playlistWithAudios.AudioPlaylists);
                _dbContext.SaveChanges();

                return ResponseBuilder<BaseResult>.SuccessBuild();
            });
        }

        public PlaylistResult CreatePlaylist(Guid userId, PlaylistRequest request)
        {
            return RequestTemplateCheckModel(request, () =>
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
                     orderby ap.Order descending
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
                playlistWithAudiosQry = playlistId.HasValue
                        ? playlistWithAudiosQry.Where(x => x.Id == playlistId.Value)
                        : playlistWithAudiosQry.Where(x => x.Owner.Id == userId && x.IsGeneral);

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
