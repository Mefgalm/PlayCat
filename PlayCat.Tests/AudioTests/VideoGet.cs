using PlayCat.DataService;
using Xunit;
using PlayCat.DataService.Response;
using PlayCat.Music.Youtube;
using System.IO;
using PlayCat.DataService.Request;
using PlayCat.Music;
using PlayCat.DataService.Request.AudioRequest;
using PlayCat.DataService.Response.AudioRequest;

namespace PlayCat.Tests.AudioTests
{
    public class VideoGet : BaseTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("123")]
        [InlineData("htt://ww.youtube.com/watch?v=Qa4u4D32x4U")]
        [InlineData("htp://www.youtube.com/watch?v=Qa4u4D32x4U")]
        [InlineData("http://ww.youtube.com/watch?v=Qa4u4D32x4U")]
        [InlineData("https://ww.youtube.com/watch?v=Qa4u4D32x4U")]
        [InlineData("https://youtu.be/Qa4u4D32x4U&param=2")]
        public void IsErrorOnInvalidUrl(string url)
        {
            var audioService = _server.Host.Services.GetService(typeof(IAudioService)) as IAudioService;

            var request = new UrlRequest()
            {
                Url = url,
            };

            GetInfoResult result = audioService.GetInfo(request);

            CheckIfFail(result);

            Assert.Equal("Model is not valid", result.Info);
            Assert.False(result.ShowInfo);
            Assert.NotNull(result.Errors);
            Assert.Equal(result.Errors.Count, 1);
        }

        [Fact]
        public void IsWrongUrlId()
        {
            var audioService = _server.Host.Services.GetService(typeof(IAudioService)) as IAudioService;

            var request = new UrlRequest()
            {
                Url = "https://www.youtube.com/watch?v=11111111",
            };

            GetInfoResult result = audioService.GetInfo(request);

            CheckIfFail(result);

            Assert.True(result.Info.Length > 0);
            Assert.Null(result.Errors);
        }

        [Fact]
        public void IsVideoSizeToLarge()
        {
            var audioService = _server.Host.Services.GetService(typeof(IAudioService)) as IAudioService;

            var request = new UrlRequest()
            {
                Url = "https://www.youtube.com/watch?v=2mI0nEgdgsA",
            };

            GetInfoResult result = audioService.GetInfo(request);

            CheckIfFail(result);

            Assert.Equal("Maximim video size is 25 MB", result.Info);
            Assert.True(result.ShowInfo);
            Assert.Null(result.Errors);
        }

        [Theory]
        [InlineData("https://www.youtube.com/watch?v=80AlC3LaPqQ")]
        [InlineData("http://www.youtube.com/watch?v=80AlC3LaPqQ")]
        [InlineData("www.youtube.com/watch?v=80AlC3LaPqQ")]
        [InlineData("youtube.com/watch?v=80AlC3LaPqQ")]
        [InlineData("http://www.youtube.com/watch?v=80AlC3LaPqQ&t=33")]
        [InlineData("https://www.youtube.com/watch?v=80AlC3LaPqQ&t=33")]
        [InlineData("https://www.youtube.com/watch?v=80AlC3LaPqQ&t=33&featured=youtu.be")]
        [InlineData("https://youtu.be/80AlC3LaPqQ")]
        [InlineData("http://youtu.be/80AlC3LaPqQ")]
        public void IsValidUrl(string url)
        {
            var audioService = _server.Host.Services.GetService(typeof(IAudioService)) as IAudioService;

            var request = new UrlRequest()
            {
                Url = url,
            };

            GetInfoResult result = audioService.GetInfo(request);

            CheckIfSuccess(result);

            Assert.NotNull(result.UrlInfo);
            Assert.Equal("Flume", result.UrlInfo.Artist);
            Assert.Equal("Say It (feat. Tove Lo) (Illenium Remix)", result.UrlInfo.Song);
            Assert.Equal(8023661, result.UrlInfo.ContentLenght);
        }
        
    }
}
