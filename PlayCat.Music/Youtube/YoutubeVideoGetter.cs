using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using YoutubeExtractor;

[assembly: InternalsVisibleTo("PlayCat.Tests")]
namespace PlayCat.Music.Youtube
{    
    public class YoutubeVideoGetter : IVideoGetter<VideoInfo, string>
    {
        private const string YoutuberRegexp = @"^(http(s)??\:\/\/)?(www\.)?(youtube\.com\/watch\?v=[A-Za-z0-9_-]+|youtu\.be\/[A-Za-z0-9_-]+)$";

        public VideoInfo GetVideoInfo(string resource)
        {
            if (resource is null)
                throw new Exception("Link can't be null or empty");

            string validUrl = resource.Trim();

            if (!new Regex(YoutuberRegexp).Match(resource).Success)
                throw new Exception("Link is not valid for youtube video");

            validUrl = RemoveParametersFromUrl(resource);

            IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(validUrl);
            return videoInfos.FirstOrDefault(info => info.VideoType == VideoType.Mp4 && info.Resolution == 360);
        }

        internal string RemoveParametersFromUrl(string url)
        {
            if (url is null)
                return null;

            if (!url.Contains('?'))
                return url;

            int firstQuestionMark = url.IndexOf('?');

            if (firstQuestionMark == -1)
                return url;

            int lastQustionMark = url.LastIndexOf('?');

            if(firstQuestionMark != lastQustionMark)
            {
                int nextQustionMark = url.IndexOf('?', firstQuestionMark + 1);

                return url.Substring(0, nextQustionMark);
            }
            return url;
        }
    }
}
