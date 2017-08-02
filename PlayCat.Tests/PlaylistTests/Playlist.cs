using PlayCat.DataService;
using PlayCat.Tests.Extensions;
using System;
using System.Linq;
using Xunit;
using PlayCat.DataService.Response.AudioRequest;

namespace PlayCat.Tests.PlaylistTests
{
    public class Playlist : BaseTest
    {        
        [Fact]
        public void ShouldReturnPlaylists()
        {
            SqlLiteDatabaseTest(options =>
            {
                var playListService = _server.Host.Services.GetService(typeof(IPlaylistService)) as IPlaylistService;

                using (var context = new PlayCatDbContext(options))
                {
                    playListService.SetDbContext(context);

                    Guid userId = GetUserId(context);
                    //create general
                    DataModel.Playlist playlist = context.CreatePlaylist(true, userId, null);

                    DataModel.Playlist playlist1 = context.CreatePlaylist(false, userId, "playlist1");
                    DataModel.Playlist playlist2 = context.CreatePlaylist(false, userId, "playlist2");
                    DataModel.Playlist playlist3 = context.CreatePlaylist(false, userId, "playlist3");

                    context.SaveChanges();

                    UserPlaylistsResult result = playListService.GetUserPlaylists(userId);

                    CheckIfSuccess(result);

                    Assert.Equal(4, result.Playlists.Count());
                }
            });
        }

        [Fact]
        public void ShouldEmptyOnWrongId()
        {
            SqlLiteDatabaseTest(options =>
            {
                var playListService = _server.Host.Services.GetService(typeof(IPlaylistService)) as IPlaylistService;

                using (var context = new PlayCatDbContext(options))
                {
                    playListService.SetDbContext(context);

                    Guid userId = GetUserId(context);

                    UserPlaylistsResult result = playListService.GetUserPlaylists(Guid.Empty);

                    CheckIfSuccess(result);

                    Assert.Equal(0, result.Playlists.Count());
                }
            });
        }

        [Fact]
        public void ShouldReturnEmpty()
        {
            SqlLiteDatabaseTest(options =>
            {
                var playListService = _server.Host.Services.GetService(typeof(IPlaylistService)) as IPlaylistService;

                using (var context = new PlayCatDbContext(options))
                {
                    playListService.SetDbContext(context);

                    Guid userId = GetUserId(context);

                    UserPlaylistsResult result = playListService.GetUserPlaylists(userId);

                    CheckIfSuccess(result);

                    Assert.Equal(0, result.Playlists.Count());
                }
            });
        }

        [Fact]
        public void IsDescendingOrder()
        {
            SqlLiteDatabaseTest(options =>
            {
                var playListService = _server.Host.Services.GetService(typeof(IPlaylistService)) as IPlaylistService;

                using (var context = new PlayCatDbContext(options))
                {
                    playListService.SetDbContext(context);

                    Guid userId = GetUserId(context);

                    DataModel.Playlist playlist = context.CreatePlaylist(true, userId, null);
                    CreateAndAddAudio(context, playlist.Id, 5);
                    context.SaveChanges();

                    PlaylistResult result = playListService.GetPlaylist(userId, playlist.Id, 0, 5);

                    CheckIfSuccess(result);

                    Assert.Equal(playlist.Id, result.Playlist.Id);
                    Assert.Equal(playlist.Title, result.Playlist.Title);
                    Assert.Equal(5, result.Playlist.Audios.Count());

                    for(int i = 0; i < 4; i++)
                    {
                        Assert.True(result.Playlist.Audios.ElementAt(i).DateAdded > result.Playlist.Audios.ElementAt(i + 1).DateAdded);
                    }
                }
            });
        }

        [Fact]
        public void ShouldReturnNotGeneralPlaylist()
        {
            SqlLiteDatabaseTest(options =>
            {
                var playListService = _server.Host.Services.GetService(typeof(IPlaylistService)) as IPlaylistService;

                using (var context = new PlayCatDbContext(options))
                {
                    playListService.SetDbContext(context);

                    Guid userId = GetUserId(context);

                    DataModel.Playlist playlist = context.CreatePlaylist(false, userId, "My playlist");
                    CreateAndAddAudio(context, playlist.Id, 10);
                    context.SaveChanges();

                    PlaylistResult result = playListService.GetPlaylist(userId, playlist.Id, 0, 10);

                    CheckIfSuccess(result);

                    Assert.Equal(playlist.Id, result.Playlist.Id);
                    Assert.Equal(playlist.Title, result.Playlist.Title);
                    Assert.Equal(10, result.Playlist.Audios.Count());
                }
            });
        }

        [Fact]
        public void ShouldReturnErrorOnWrongPlaylistId()
        {
            SqlLiteDatabaseTest(options =>
            {
                var playListService = _server.Host.Services.GetService(typeof(IPlaylistService)) as IPlaylistService;

                using (var context = new PlayCatDbContext(options))
                {
                    playListService.SetDbContext(context);

                    Guid userId = GetUserId(context);

                    DataModel.Playlist playlist = context.CreatePlaylist(true, userId, null);
                    CreateAndAddAudio(context, playlist.Id, 10);
                    context.SaveChanges();

                    PlaylistResult result = playListService.GetPlaylist(userId, Guid.Empty, 0, 10);

                    CheckIfFail(result);

                    Assert.Null(result.Playlist);
                }
            });
        }

        [Fact]
        public void ShouldReturnGeneralPlaylist()
        {
            SqlLiteDatabaseTest(options =>
            {
                var playListService = _server.Host.Services.GetService(typeof(IPlaylistService)) as IPlaylistService;

                using (var context = new PlayCatDbContext(options))
                {
                    playListService.SetDbContext(context);

                    Guid userId = GetUserId(context);

                    DataModel.Playlist playlist = context.CreatePlaylist(true, userId, null);
                    CreateAndAddAudio(context, playlist.Id, 10);
                    context.SaveChanges();

                    PlaylistResult result = playListService.GetPlaylist(userId, null, 0, 10);

                    CheckIfSuccess(result);

                    Assert.Equal(playlist.Id, result.Playlist.Id);
                    Assert.Equal(playlist.Title, result.Playlist.Title);
                    Assert.Equal(10, result.Playlist.Audios.Count());
                }
            });
        }

        [Theory]
        [InlineData(0, 0, 0, 0)]
        [InlineData(0, 0, 10, 0)]
        [InlineData(10, 0, 10, 10)]
        [InlineData(20, 0, 10, 10)]
        [InlineData(5, 0, 10, 5)]
        [InlineData(10, 5, 10, 5)]
        [InlineData(10, -10, 10, 10)]
        [InlineData(10, 0, -10, 10)]
        [InlineData(10, -10, -10, 10)]
        public void IsGetAllAudios(int count, int skip, int take, int actualCount)
        {
            SqlLiteDatabaseTest(options =>
            {
                var playListService = _server.Host.Services.GetService(typeof(IPlaylistService)) as IPlaylistService;

                using (var context = new PlayCatDbContext(options))
                {
                    playListService.SetDbContext(context);

                    Guid userId = GetUserId(context);

                    DataModel.Playlist playlist = context.CreatePlaylist(true, userId, null);
                    CreateAndAddAudio(context, playlist.Id, count);
                    context.SaveChanges();

                    PlaylistResult result = playListService.GetPlaylist(userId, playlist.Id, skip, take);

                    CheckIfSuccess(result);

                    Assert.Equal(playlist.Id, result.Playlist.Id);
                    Assert.Equal(playlist.Title, result.Playlist.Title);
                    Assert.Equal(actualCount, result.Playlist.Audios.Count());
                }
            });
        }

        private Guid GetUserId(PlayCatDbContext context)
        {
            var inviteService = _server.Host.Services.GetService(typeof(IInviteService)) as IInviteService;

            string password = "123456abc";
            string email = "test@gmail.com";

            DataModel.User user = context.CreateUser(email, "test", "test", password, inviteService.GenerateInvite());
            DataModel.AuthToken authToken = context.CreateToken(DateTime.Now.AddDays(-1), false, user.Id);            

            context.SaveChanges();

            return user.Id;
        }

        private void CreateAndAddAudio(PlayCatDbContext context, Guid playlistId, int count)
        {
            if (count < -0) return;

            for(int i = 0; i < count; i++)
            {
                DataModel.Audio audio = context.CreateAudio(DateTime.Now.AddMinutes(i), "access" + i, "artist" + i, "song" + i, BaseAudioExtension, i.ToString(), i.ToString(), null);

                DataModel.AudioPlaylist audioPlaylist = context.CreateAudioPlaylist(DateTime.Now.AddMinutes(i), audio.Id, playlistId);
            }
        }
    }
}
