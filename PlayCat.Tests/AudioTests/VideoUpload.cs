using PlayCat.DataService;
using PlayCat.DataService.Request;
using PlayCat.DataService.Response;
using PlayCat.Music;
using System;
using System.IO;
using System.Linq;
using System.Web.Helpers;
using Xunit;

namespace PlayCat.Tests.AudioTests
{
    public class VideoUpload : BaseTest
    {
        private Guid GetUserId(PlayCatDbContext context)
        {
            var inviteService = _server.Host.Services.GetService(typeof(IInviteService)) as IInviteService;

            string password = "123456abc";
            string email = "test@gmail.com";

            string salt = Crypto.GenerateSalt();
            string passwordHah = Crypto.HashPassword(password + salt);

            var user = context.Users.Add(new DataModel.User()
            {
                Id = Guid.NewGuid(),
                Email = email,
                FirstName = "test",
                LastName = "test",
                PasswordHash = passwordHah,
                PasswordSalt = salt,
                RegisterDate = DateTime.Now,
                VerificationCode = inviteService.GenerateInvite(),
            });

            var generalPlaylist = context.Playlists.Add(new DataModel.Playlist()
            {
                Id = Guid.NewGuid(),
                IsGeneral = true,
                UserId = user.Entity.Id,
            });

            var authToken = context.AuthTokens.Add(new DataModel.AuthToken()
            {
                Id = Guid.NewGuid(),
                DateExpired = DateTime.Now.AddDays(-1),
                IsActive = false,
                UserId = user.Entity.Id,
            });

            context.SaveChanges();

            return user.Entity.Id;
        }

        [Fact]
        public void IsInvalidModel()
        {
            var audioService = _server.Host.Services.GetService(typeof(IAudioService)) as IAudioService;

            var uploadAudioRequest = new UploadAudioRequest();

            BaseResult result = audioService.UploadAudio(Guid.Empty, uploadAudioRequest);

            CheckIfFail(result);

            Assert.Equal("Model is not valid", result.Info);
            Assert.False(result.ShowInfo);
            Assert.NotNull(result.Errors);
            Assert.Equal(result.Errors.Count, 3);
        }

        [Fact]
        public void IsAlreadyExists()
        {
            SqlLiteDatabaseTest(options =>
            {
                var audioService = _server.Host.Services.GetService(typeof(IAudioService)) as IAudioService;
                var fileResolver = _server.Host.Services.GetService(typeof(IFileResolver)) as IFileResolver;

                using (var context = new PlayCatDbContext(options))
                {
                    audioService.SetDbContext(context);

                    var uploadAudioRequest = new UploadAudioRequest()
                    {
                        Artist = "Flume",
                        Song = "Say It (feat. Tove Lo) (Illenium Remix)",
                        Url = "https://www.youtube.com/watch?v=80AlC3LaPqQ",
                    };

                    Guid userId = GetUserId(context);

                    var result = audioService.UploadAudio(userId, uploadAudioRequest);
                    var resultDownloaded = audioService.UploadAudio(userId, uploadAudioRequest);

                    CheckIfFail(resultDownloaded);

                    Assert.Equal("Video already uploaded", resultDownloaded.Info);

                    string audioFilePath = fileResolver.GetAudioFolderPath(StorageType.FileSystem);
                    string videoFilePath = fileResolver.GetVideoFolderPath(StorageType.FileSystem);

                    Assert.True(File.Exists(Path.Combine(audioFilePath, "80AlC3LaPqQ.mp3")));
                    Assert.False(File.Exists(Path.Combine(videoFilePath, "80AlC3LaPqQ.mp4")));

                    File.Delete(Path.Combine(audioFilePath, "80AlC3LaPqQ.mp3"));
                }
            });
        }

        [Fact]
        public void IsValidUrl()
        {
            SqlLiteDatabaseTest(options =>
            {
                var audioService = _server.Host.Services.GetService(typeof(IAudioService)) as IAudioService;
                var fileResolver = _server.Host.Services.GetService(typeof(IFileResolver)) as IFileResolver;

                using (var context = new PlayCatDbContext(options))
                {
                    audioService.SetDbContext(context);

                    var uploadAudioRequest = new UploadAudioRequest()
                    {
                        Artist = "Flume",
                        Song = "Say It (feat. Tove Lo) (Illenium Remix)",
                        Url = "https://www.youtube.com/watch?v=80AlC3LaPqQ",
                    };

                    Guid userId = GetUserId(context);
                    var result = audioService.UploadAudio(userId, uploadAudioRequest);

                    CheckIfSuccess(result);

                    var audio = context.Audios.Single();

                    Assert.Equal("Say It (feat. Tove Lo) (Illenium Remix)", audio.Song);
                    Assert.Equal("Flume", audio.Artist);
                    Assert.Equal("80AlC3LaPqQ", audio.UniqueIdentifier);

                    var audioPlaylists = context.AudioPlaylists.Single();
                    var generalPlaylist = context.Playlists.Single();

                    Assert.True(generalPlaylist.IsGeneral);
                    Assert.Equal(generalPlaylist.UserId, userId);

                    Assert.Equal(audioPlaylists.AudioId, audio.Id);
                    Assert.Equal(audioPlaylists.PlaylistId, generalPlaylist.Id);

                    string audioFilePath = fileResolver.GetAudioFolderPath(StorageType.FileSystem);
                    string videoFilePath = fileResolver.GetVideoFolderPath(StorageType.FileSystem);

                    Assert.True(File.Exists(Path.Combine(audioFilePath, "80AlC3LaPqQ.mp3")));
                    Assert.False(File.Exists(Path.Combine(videoFilePath, "80AlC3LaPqQ.mp4")));

                    File.Delete(Path.Combine(audioFilePath, "80AlC3LaPqQ.mp3"));
                }
            });
        }             
    }
}
