using PlayCat.DataService;
using PlayCat.DataService.Request;
using PlayCat.DataService.Response;
using PlayCat.DataService.Response.UploadResponse;
using PlayCat.Music;
using PlayCat.Tests.Extensions;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Helpers;
using Xunit;

namespace PlayCat.Tests.UploadTests
{
    public class VideoUpload : BaseTest
    {
        private new Guid GetUserId(PlayCatDbContext context)
        {
            var inviteService = _server.Host.Services.GetService(typeof(IInviteService)) as IInviteService;

            string password = "123456abc";
            string email = "test@gmail.com";

            DataModel.User user = context.CreateUser(email, "test", "test", password, inviteService.GenerateInvite());
            DataModel.Playlist playlist = context.CreatePlaylist(true, user.Id, "General", 0);
            DataModel.AuthToken authToken = context.CreateToken(DateTime.Now.AddDays(-1), false, user.Id);

            context.SaveChanges();

            return user.Id;
        }

        [Fact]
        public void IsInvalidModel()
        {
            var uploadService = _server.Host.Services.GetService(typeof(IUploadService)) as IUploadService;

            var uploadAudioRequest = new UploadAudioRequest();
            
            UploadResult result = uploadService.UploadAudio(Guid.Empty, uploadAudioRequest);

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
                var uploadService = _server.Host.Services.GetService(typeof(IUploadService)) as IUploadService;
                var fileResolver = _server.Host.Services.GetService(typeof(IFileResolver)) as IFileResolver;

                using (var context = new PlayCatDbContext(options))
                {
                    uploadService.SetDbContext(context);

                    var uploadAudioRequest = new UploadAudioRequest()
                    {
                        Artist = "Flume",
                        Song = "Say It (feat. Tove Lo) (Illenium Remix)",
                        Url = "https://www.youtube.com/watch?v=80AlC3LaPqQ",
                    };

                    Guid userId = GetUserId(context);

                    UploadResult result = uploadService.UploadAudio(userId, uploadAudioRequest);
                    UploadResult resultDownloaded = uploadService.UploadAudio(userId, uploadAudioRequest);

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
        public void ShouldFailOnUploadInOneTime()
        {
            SqlLiteDatabaseTest(options =>
            {
                var uploadService = _server.Host.Services.GetService(typeof(IUploadService)) as IUploadService;
                var fileResolver = _server.Host.Services.GetService(typeof(IFileResolver)) as IFileResolver;

                using (var context = new PlayCatDbContext(options))
                {
                    uploadService.SetDbContext(context);

                    var uploadAudioRequest = new UploadAudioRequest()
                    {
                        Artist = "Flume",
                        Song = "Say It (feat. Tove Lo) (Illenium Remix)",
                        Url = "https://www.youtube.com/watch?v=80AlC3LaPqQ",
                    };

                    Guid userId = GetUserId(context);

                    Task.Run(() => {

                        uploadService.UploadAudio(userId, uploadAudioRequest);

                        string audioFilePath = fileResolver.GetAudioFolderPath(StorageType.FileSystem);

                        File.Delete(Path.Combine(audioFilePath, "80AlC3LaPqQ.mp3"));
                    });
                    Thread.Sleep(500);
                    UploadResult result = uploadService.UploadAudio(userId, uploadAudioRequest);                    

                    CheckIfFail(result);
                    Assert.Equal("User already uploading audio", result.Info);
                }
            });
        }

        [Fact]
        public void IsValidUrl()
        {
            SqlLiteDatabaseTest(options =>
            {
                var uploadService = _server.Host.Services.GetService(typeof(IUploadService)) as IUploadService;
                var fileResolver = _server.Host.Services.GetService(typeof(IFileResolver)) as IFileResolver;

                using (var context = new PlayCatDbContext(options))
                {
                    uploadService.SetDbContext(context);

                    var uploadAudioRequest = new UploadAudioRequest()
                    {
                        Artist = "Flume",
                        Song = "Say It (feat. Tove Lo) (Illenium Remix)",
                        Url = "https://www.youtube.com/watch?v=80AlC3LaPqQ",
                    };

                    Guid userId = GetUserId(context);
                    UploadResult result = uploadService.UploadAudio(userId, uploadAudioRequest);

                    CheckIfSuccess(result);

                    var audio = context.Audios.Single();

                    Assert.Equal("Say It (feat. Tove Lo) (Illenium Remix)", result.Audio.Song);
                    Assert.Equal("Flume", result.Audio.Artist);
                    Assert.Equal("80AlC3LaPqQ", audio.UniqueIdentifier);

                    var audioPlaylists = context.AudioPlaylists.Single();
                    var generalPlaylist = context.Playlists.Single();

                    Assert.True(generalPlaylist.IsGeneral);
                    Assert.Equal(generalPlaylist.OwnerId, userId);

                    Assert.Equal(generalPlaylist.OrderValue, 1);
                    Assert.Equal(audioPlaylists.Order, 0);

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
