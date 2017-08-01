using PlayCat.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using YoutubeExtractor;

namespace PlayCat.Music.Youtube
{
    public class YoutubeVideoInfoGetter : IVideoInfoGetter
    {
        private const string YoutuberRegexp = @"^(http(s)??\:\/\/)?(www\.)?(youtube\.com\/watch\?v=[\.A-Za-z0-9_\?=&-]+|youtu\.be\/[A-Za-z0-9_-]+)$";

        public IUrlInfo GetInfo(string url)
        {
            if (url == null)
                throw new ArgumentNullException(nameof(url));

            url = url.Trim();

            if (!new Regex(YoutuberRegexp).Match(url).Success)
                throw new Exception("Link is not valid for youtube video");

            url = UrlFormatter.RemoveParametersFromUrl(url);

            IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(url);
            VideoInfo videoInfo = videoInfos.First(info => info.VideoType == VideoType.Mp4 && info.Resolution == 360);

            Headers headers = HttpRequester.GetHeaders(videoInfo.DownloadUrl);
            var artistAndSong = GetArtistAndSongName(videoInfo.Title);

            return new UrlInfo()
            {
               Artist = artistAndSong.Artist,
               Song = artistAndSong.Song,
               ContentLenght = headers.ContentLenght,
            };
        }

        private (string Artist, string Song) GetArtistAndSongName(string title)
        {
            if (title == null)
                return (string.Empty, string.Empty);

            string[] artistAndSong = title.Split('-');
            if (artistAndSong.Length >= 2)
            {
                return (artistAndSong[0].Trim(), artistAndSong[1].Trim());
            }

            return (title, string.Empty);
        }
    }
}
