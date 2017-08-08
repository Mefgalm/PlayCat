using PlayCat.DataService;
using PlayCat.DataService.Request;
using PlayCat.DataService.Response;
using PlayCat.Tests.Extensions;
using System;
using System.Linq;
using Xunit;

namespace PlayCat.Tests.AudioTests
{
    public class Audio : BaseTest
    {
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
        public void ShouldReturnAudios(int count, int skip, int take, int actual)
        {
            SqlLiteDatabaseTest(options =>
            {
                var audioService = _server.Host.Services.GetService(typeof(IAudioService)) as IAudioService;

                using (var context = new PlayCatDbContext(options))
                {
                    audioService.SetDbContext(context);

                    Guid userId = GetUserId(context);

                    DataModel.Playlist playlist = context.CreatePlaylist(true, userId, null, 0);
                    CreateAndAddAudio(context, playlist.Id, count);
                    context.SaveChanges();

                    AudioResult result = audioService.GetAudios(playlist.Id, skip, take);

                    CheckIfSuccess(result);

                    Assert.Equal(actual, result.Audios.Count());
                }
            });
        }

        [Fact]
        public void ShouldFailRemoveFromPlaylistNotOwner()
        {
            SqlLiteDatabaseTest(options =>
            {
                var audioService = _server.Host.Services.GetService(typeof(IAudioService)) as IAudioService;

                using (var context = new PlayCatDbContext(options))
                {
                    audioService.SetDbContext(context);

                    Guid userId = GetUserId(context);
                    DataModel.Playlist generalPlaylist = context.CreatePlaylist(true, userId, null, 0);

                    DataModel.Playlist playlist = context.CreatePlaylist(false, userId, "Rock", 0);
                    CreateAndAddAudio(context, playlist.Id, 1);
                    context.SaveChanges();

                    var audio = context.Audios.FirstOrDefault();

                    var result = audioService.RemoveFromPlaylist(Guid.Empty, new AddRemovePlaylistRequest()
                    {
                        AudioId = audio.Id,
                        PlaylistId = playlist.Id,
                    });

                    CheckIfFail(result);

                    Assert.True(result.Info.Any());
                }
            });
        }

        [Fact]
        public void ShouldFailAddToPlaylistNotOwner()
        {
            SqlLiteDatabaseTest(options =>
            {
                var audioService = _server.Host.Services.GetService(typeof(IAudioService)) as IAudioService;

                using (var context = new PlayCatDbContext(options))
                {
                    audioService.SetDbContext(context);

                    Guid userId = GetUserId(context);
                    DataModel.Playlist generalPlaylist = context.CreatePlaylist(true, userId, null, 0);

                    DataModel.Playlist playlist = context.CreatePlaylist(false, userId, "Rock", 0);
                    CreateAndAddAudio(context, playlist.Id, 1);
                    context.SaveChanges();

                    var audio = context.Audios.FirstOrDefault();

                    var result = audioService.AddToPlaylist(Guid.Empty, new AddRemovePlaylistRequest()
                    {
                        AudioId = audio.Id,
                        PlaylistId = playlist.Id,
                    });

                    CheckIfFail(result);

                    Assert.True(result.Info.Any());
                }
            });
        }

        [Fact]
        public void ShouldRemoveFromPlaylist()
        {
            SqlLiteDatabaseTest(options =>
            {
                var audioService = _server.Host.Services.GetService(typeof(IAudioService)) as IAudioService;

                using (var context = new PlayCatDbContext(options))
                {
                    audioService.SetDbContext(context);

                    Guid userId = GetUserId(context);
                    DataModel.Playlist generalPlaylist = context.CreatePlaylist(true, userId, null, 0);                    

                    DataModel.Playlist playlist = context.CreatePlaylist(false, userId, "Rock", 0);
                    CreateAndAddAudio(context, playlist.Id, 1);
                    context.SaveChanges();

                    var audio = context.Audios.FirstOrDefault();

                    var result = audioService.RemoveFromPlaylist(userId, new AddRemovePlaylistRequest()
                    {
                        AudioId = audio.Id,
                        PlaylistId = playlist.Id,
                    });

                    CheckIfSuccess(result);

                    Assert.Equal(context.AudioPlaylists.Count(), 0);
                }
            });
        }

        [Fact]
        public void ShouldFailRemoveFromGeneral()
        {
            SqlLiteDatabaseTest(options =>
            {
                var audioService = _server.Host.Services.GetService(typeof(IAudioService)) as IAudioService;

                using (var context = new PlayCatDbContext(options))
                {
                    audioService.SetDbContext(context);

                    Guid userId = GetUserId(context);
                    DataModel.Playlist generalPlaylist = context.CreatePlaylist(true, userId, null, 0);

                    CreateAndAddAudio(context, generalPlaylist.Id, 1);
                    context.SaveChanges();

                    var audio = context.Audios.FirstOrDefault();

                    var result = audioService.RemoveFromPlaylist(userId, new AddRemovePlaylistRequest()
                    {
                        AudioId = audio.Id,
                        PlaylistId = generalPlaylist.Id,
                    });

                    CheckIfFail(result);
                    Assert.Equal(context.AudioPlaylists.Count(), 1);
                    Assert.True(result.Info.Any());
                }
            });
        }

        [Fact]
        public void ShouldRemoveIfAudioNotExists()
        {
            SqlLiteDatabaseTest(options =>
            {
                var audioService = _server.Host.Services.GetService(typeof(IAudioService)) as IAudioService;

                using (var context = new PlayCatDbContext(options))
                {
                    audioService.SetDbContext(context);

                    Guid userId = GetUserId(context);
                    DataModel.Playlist generalPlaylist = context.CreatePlaylist(true, userId, null, 0);

                    DataModel.Playlist playlist = context.CreatePlaylist(false, userId, "Rock", 0);
                    context.SaveChanges();

                    var result = audioService.RemoveFromPlaylist(userId, new AddRemovePlaylistRequest()
                    {
                        AudioId = Guid.Empty,
                        PlaylistId = playlist.Id,
                    });

                    CheckIfSuccess(result);
                }
            });
        }

        [Fact]
        public void ShouldFailOnWrongPlaylist()
        {
            SqlLiteDatabaseTest(options =>
            {
                var audioService = _server.Host.Services.GetService(typeof(IAudioService)) as IAudioService;

                using (var context = new PlayCatDbContext(options))
                {
                    audioService.SetDbContext(context);

                    Guid userId = GetUserId(context);
                    DataModel.Playlist generalPlaylist = context.CreatePlaylist(true, userId, null, 0);
                    CreateAndAddAudio(context, generalPlaylist.Id, 10);

                    DataModel.Playlist playlist = context.CreatePlaylist(false, userId, "Rock", 0);
                    context.SaveChanges();

                    var audio = context.Audios.FirstOrDefault();

                    var result = audioService.AddToPlaylist(userId, new AddRemovePlaylistRequest()
                    {
                        AudioId = audio.Id,
                        PlaylistId = Guid.Empty,
                    });

                    CheckIfFail(result);
                    Assert.True(result.Info.Any());
                }
            });
        }

        [Fact]
        public void ShouldFailOnAddToGeneral()
        {
            SqlLiteDatabaseTest(options =>
            {
                var audioService = _server.Host.Services.GetService(typeof(IAudioService)) as IAudioService;

                using (var context = new PlayCatDbContext(options))
                {
                    audioService.SetDbContext(context);

                    Guid userId = GetUserId(context);
                    DataModel.Playlist generalPlaylist = context.CreatePlaylist(true, userId, null, 0);
                    CreateAndAddAudio(context, generalPlaylist.Id, 10);

                    context.SaveChanges();

                    var audio = context.Audios.FirstOrDefault();

                    var result = audioService.AddToPlaylist(userId, new AddRemovePlaylistRequest()
                    {
                        AudioId = audio.Id,
                        PlaylistId = generalPlaylist.Id,
                    });

                    CheckIfFail(result);
                    Assert.True(result.Info.Any());
                }
            });
        }

        [Fact]
        public void ShouldFailOnAlreadyAdded()
        {
            SqlLiteDatabaseTest(options =>
            {
                var audioService = _server.Host.Services.GetService(typeof(IAudioService)) as IAudioService;

                using (var context = new PlayCatDbContext(options))
                {
                    audioService.SetDbContext(context);

                    Guid userId = GetUserId(context);
                    DataModel.Playlist generalPlaylist = context.CreatePlaylist(true, userId, null, 0);
                    CreateAndAddAudio(context, generalPlaylist.Id, 10);

                    DataModel.Playlist playlist = context.CreatePlaylist(false, userId, "Rock", 0);
                    context.SaveChanges();

                    var audio = context.Audios.FirstOrDefault();

                    var result = audioService.AddToPlaylist(userId, new AddRemovePlaylistRequest()
                    {
                        AudioId = audio.Id,
                        PlaylistId = playlist.Id,
                    });
                    result = audioService.AddToPlaylist(userId, new AddRemovePlaylistRequest()
                    {
                        AudioId = audio.Id,
                        PlaylistId = playlist.Id,
                    });

                    CheckIfFail(result);
                    Assert.True(result.Info.Any());
                }
            });
        }

        [Fact]
        public void ShouldAddToPlaylist()
        {
            SqlLiteDatabaseTest(options =>
            {
                var audioService = _server.Host.Services.GetService(typeof(IAudioService)) as IAudioService;

                using (var context = new PlayCatDbContext(options))
                {
                    audioService.SetDbContext(context);

                    Guid userId = GetUserId(context);
                    DataModel.Playlist generalPlaylist = context.CreatePlaylist(true, userId, null, 0);
                    CreateAndAddAudio(context, generalPlaylist.Id, 10);

                    DataModel.Playlist playlist = context.CreatePlaylist(false, userId, "Rock", 0);
                    context.SaveChanges();

                    var audio = context.Audios.FirstOrDefault();

                    var result = audioService.AddToPlaylist(userId, new AddRemovePlaylistRequest()
                    {
                        AudioId = audio.Id,
                        PlaylistId = playlist.Id,
                    });

                    var audioPlaylists = context.AudioPlaylists.ToList();
                    var addedAP = audioPlaylists.FirstOrDefault(x => x.PlaylistId == playlist.Id);

                    CheckIfSuccess(result);
                    Assert.Equal(audioPlaylists.Count, 11);
                    Assert.NotNull(addedAP);
                    Assert.Equal(addedAP.AudioId, audio.Id);
                }
            });
        }

        [Fact]
        public void SearchByStartsWithAndOrder()
        {
            SqlLiteDatabaseTest(options =>
            {
                var audioService = _server.Host.Services.GetService(typeof(IAudioService)) as IAudioService;

                using (var context = new PlayCatDbContext(options))
                {
                    audioService.SetDbContext(context);

                    context.CreateAudio(DateTime.Now, "0", artist: "B", song: "B", extension: "", filename: "", videoId: "A", uploaderId: null);
                    context.CreateAudio(DateTime.Now, "1", artist: "B", song: "A", extension: "", filename: "", videoId: "B", uploaderId: null);
                    context.CreateAudio(DateTime.Now, "2", artist: "A", song: "B", extension: "", filename: "", videoId: "C", uploaderId: null);

                    context.SaveChanges();

                    var result = audioService.SearchAudios("A", 0, 10);

                    CheckIfSuccess(result);
                    Assert.Equal(result.Audios.Count(), 3);
                    Assert.Equal(result.Audios.ElementAt(0).AccessUrl, "0");
                    Assert.Equal(result.Audios.ElementAt(1).AccessUrl, "1");
                    Assert.Equal(result.Audios.ElementAt(2).AccessUrl, "2");
                }
            });
        }

        [Fact]
        public void SearchByComposeArtistAndSong()
        {
            SqlLiteDatabaseTest(options =>
            {
                var audioService = _server.Host.Services.GetService(typeof(IAudioService)) as IAudioService;

                using (var context = new PlayCatDbContext(options))
                {
                    audioService.SetDbContext(context);

                    context.CreateAudio(DateTime.Now, "0", artist: "A", song: "B", extension: "", filename: "", videoId: "", uploaderId: null);
                    context.SaveChanges();

                    var result = audioService.SearchAudios("A B", 0, 10);

                    CheckIfSuccess(result);
                    Assert.Equal(result.Audios.Count(), 1);
                }
            });
        }

        [Fact]
        public void SearchPaging()
        {
            SqlLiteDatabaseTest(options =>
            {
                var audioService = _server.Host.Services.GetService(typeof(IAudioService)) as IAudioService;

                using (var context = new PlayCatDbContext(options))
                {
                    audioService.SetDbContext(context);

                    context.CreateAudio(DateTime.Now, "0", artist: "B", song: "B", extension: "", filename: "", videoId: "A", uploaderId: null);
                    context.CreateAudio(DateTime.Now, "1", artist: "B", song: "A", extension: "", filename: "", videoId: "B", uploaderId: null);
                    context.CreateAudio(DateTime.Now, "2", artist: "A", song: "B", extension: "", filename: "", videoId: "C", uploaderId: null);
                    context.SaveChanges();

                    var result = audioService.SearchAudios("A", 1, 1);

                    CheckIfSuccess(result);
                    Assert.Equal(result.Audios.Count(), 1);
                    Assert.Equal(result.Audios.ElementAt(0).AccessUrl, "1");
                }
            });
        }

        [Fact]
        public void ShouldEmptyOnWrongId()
        {
            SqlLiteDatabaseTest(options =>
            {
                var audioService = _server.Host.Services.GetService(typeof(IAudioService)) as IAudioService;

                using (var context = new PlayCatDbContext(options))
                {
                    audioService.SetDbContext(context);

                    Guid userId = GetUserId(context);

                    DataModel.Playlist playlist = context.CreatePlaylist(true, userId, null, 0);
                    CreateAndAddAudio(context, playlist.Id, 10);
                    context.SaveChanges();

                    AudioResult result = audioService.GetAudios(Guid.Empty, 0, 10);

                    CheckIfSuccess(result);
                    Assert.Equal(0, result.Audios.Count());
                }
            });
        }

        private void CreateAndAddAudio(PlayCatDbContext context, Guid playlistId, int count)
        {
            if (count < -0) return;

            for (int i = 0; i < count; i++)
            {
                DataModel.Audio audio = context.CreateAudio(DateTime.Now.AddMinutes(i), "access" + i, "artist" + i, "song" + i, BaseAudioExtension, i.ToString(), i.ToString(), null);

                DataModel.AudioPlaylist audioPlaylist = context.CreateAudioPlaylist(DateTime.Now.AddMinutes(i), audio.Id, playlistId, i);
            }
        }
    }
}
