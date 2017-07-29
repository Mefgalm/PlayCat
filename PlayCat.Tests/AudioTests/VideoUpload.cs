using PlayCat.DataService;
using PlayCat.DataService.Request;
using PlayCat.DataService.Response;
using PlayCat.Music;
using System.IO;
using System.Linq;
using Xunit;

namespace PlayCat.Tests.AudioTests
{
    public class VideoUpload : BaseTest
    {
        [Fact]
        public void IsInvalidModel()
        {
            var audioService = _server.Host.Services.GetService(typeof(IAudioService)) as IAudioService;

            var uploadAudioRequest = new UploadAudioRequest();

            BaseResult result = audioService.UploadAudio(uploadAudioRequest);

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

                    var result = audioService.UploadAudio(uploadAudioRequest);
                    var resultDownloaded = audioService.UploadAudio(uploadAudioRequest);

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

                    var result = audioService.UploadAudio(uploadAudioRequest);

                    CheckIfSuccess(result);

                    var audios = context.Audios.ToList();

                    var audio = audios.First();
                    Assert.Equal(audios.Count, 1);
                    Assert.Equal("Say It (feat. Tove Lo) (Illenium Remix)", audio.Song);
                    Assert.Equal("Flume", audio.Artist);
                    Assert.Equal("80AlC3LaPqQ", audio.UniqueIdentifier);

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
