using PlayCat.DataService;
using Xunit;
using PlayCat.DataService.Response;
using PlayCat.Music.Youtube;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace PlayCat.Tests
{
    public class YoutubeUpload : BaseTest
    {
        [Fact]
        public void IsErrorOnNull()
        {
            var audioService = _server.Host.Services.GetService(typeof(IAudioService)) as IAudioService;

            string youtubeUrl = null;

            UploadAudioResult result = audioService.UploadAudio(youtubeUrl);

            Assert.NotNull(result);
            Assert.False(result.Ok);
            Assert.Equal("Link can't be null or empty", result.Info);
            Assert.Null(result.UploadFile);
        }

        [Theory]
        [InlineData("")]
        [InlineData("https://www.yo")]
        [InlineData("https://youtu.be/MI4g6vri2OA?t=34")]
        public void IsErrorOnInvalidUrl(string youtubeUrl)
        {            
            var audioService = _server.Host.Services.GetService(typeof(IAudioService)) as IAudioService;

            UploadAudioResult result = audioService.UploadAudio(youtubeUrl);

            Assert.NotNull(result);
            Assert.False(result.Ok);
            Assert.Equal("Link is not valid for youtube video", result.Info);
            Assert.Null(result.UploadFile);
        }

        [Fact]
        public void IsErrorOnLongVideoUrl()
        {
            var audioService = _server.Host.Services.GetService(typeof(IAudioService)) as IAudioService;

            string youtubeUrl = "https://www.youtube.com/watch?v=wM_ys62SJFw";

            UploadAudioResult result = audioService.UploadAudio(youtubeUrl);

            Assert.NotNull(result);
            Assert.False(result.Ok);
            Assert.Equal("Maximim size in 25 MB", result.Info);
            Assert.Null(result.UploadFile);
        }

        [Fact]
        public void IsValidFullUrl()
        {
            var audioService = _server.Host.Services.GetService(typeof(IAudioService)) as IAudioService;

            string youtubeUrl = "https://www.youtube.com/watch?v=ekEYX_yo_wE";

            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<PlayCatDbContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new PlayCatDbContext(options))
                {
                    context.Database.EnsureCreated();
                }

                // Run the test against one instance of the context
                using (var context = new PlayCatDbContext(options))
                {
                    audioService.SetDbContext(context);

                    UploadAudioResult result = audioService.UploadAudio(youtubeUrl);

                    Assert.NotNull(result);
                    Assert.True(result.Ok);
                    Assert.Null(result.Info);

                    Assert.NotNull(result.UploadFile);
                    Assert.Equal("\\Audio\\ekEYX_yo_wE.mp3", result.UploadFile.AccessUrl);
                    Assert.Equal("Alex Lewis", result.UploadFile.Artist);
                    Assert.Equal("Just So Right (Funk LeBlanc Remix)", result.UploadFile.Song);
                    Assert.Equal(".mp3", result.UploadFile.Extension);
                    Assert.Equal("ekEYX_yo_wE", result.UploadFile.FileName);
                    Assert.Equal("ekEYX_yo_wE", result.UploadFile.VideoId);
                    Assert.EndsWith("\\Audio\\ekEYX_yo_wE.mp3", result.UploadFile.PhysicUrl);

                    Assert.True(File.Exists(result.UploadFile.PhysicUrl));

                    string videoUrl = result.UploadFile.PhysicUrl.Replace("Audio", "Video").Replace("mp3", "mp4");
                    Assert.False(File.Exists(videoUrl));
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void IsValidFullUrlAlreadyExists()
        {
            var audioService = _server.Host.Services.GetService(typeof(IAudioService)) as IAudioService;

            string youtubeUrl = "https://www.youtube.com/watch?v=xkFZn4oPMqE";

            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<PlayCatDbContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new PlayCatDbContext(options))
                {
                    context.Database.EnsureCreated();
                }

                // Run the test against one instance of the context
                using (var context = new PlayCatDbContext(options))
                {
                    audioService.SetDbContext(context);

                    UploadAudioResult result = audioService.UploadAudio(youtubeUrl);

                    Assert.NotNull(result);
                    Assert.True(result.Ok);
                    Assert.Null(result.Info);

                    Assert.NotNull(result.UploadFile);
                    Assert.Equal("\\Audio\\xkFZn4oPMqE.mp3", result.UploadFile.AccessUrl);
                    Assert.Equal("Coyote Kisses", result.UploadFile.Artist);
                    Assert.Equal("Six Shooter", result.UploadFile.Song);
                    Assert.Equal(".mp3", result.UploadFile.Extension);
                    Assert.Equal("xkFZn4oPMqE", result.UploadFile.FileName);
                    Assert.Equal("xkFZn4oPMqE", result.UploadFile.VideoId);
                    Assert.EndsWith("\\Audio\\xkFZn4oPMqE.mp3", result.UploadFile.PhysicUrl);

                    Assert.True(File.Exists(result.UploadFile.PhysicUrl));

                    string videoUrl = result.UploadFile.PhysicUrl.Replace("Audio", "Video").Replace("mp3", "mp4");
                    Assert.False(File.Exists(videoUrl));

                    UploadAudioResult result2 = audioService.UploadAudio(youtubeUrl);

                    Assert.False(result2.Ok);
                    Assert.Null(result2.UploadFile);
                    Assert.Equal("Video already uploaded", result2.Info);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void IsRemoveParametersWithNull()
        {
            var youtubeVideoGetter = new YoutubeVideoGetter();

            string videoTitle = null;

            string result = youtubeVideoGetter.RemoveParametersFromUrl(videoTitle);

            Assert.Null(result);
        }

        [Theory]
        [InlineData("https://www.youtube.com/watch?v=FkMdirdJo2g")]
        [InlineData("https://www.youtube.com/watch?v=FkMdirdJo2g?")]
        [InlineData("https://www.youtube.com/watch?v=FkMdirdJo2g?t=3")]
        [InlineData("https://www.youtube.com/watch?v=FkMdirdJo2g?t=3?version=454")]
        public void IsRemoveParameters(string videoUrl)
        {
            var youtubeVideoGetter = new YoutubeVideoGetter();
            string result = youtubeVideoGetter.RemoveParametersFromUrl(videoUrl);
            Assert.Equal("https://www.youtube.com/watch?v=FkMdirdJo2g", result);
        }

        [Fact()]        
        public void IsRemoveParametersForShortLink()
        {
            var youtubeVideoGetter = new YoutubeVideoGetter();
            string result = youtubeVideoGetter.RemoveParametersFromUrl("https://youtu.be/MI4g6vri2OA");
            Assert.Equal("https://youtu.be/MI4g6vri2OA", result);
        }
    }
}
