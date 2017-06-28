using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using YoutubeExtractor;

namespace PlayCat.Music.Youtube
{
    public class YoutubeVideoGetter : IVideoGetter<VideoInfo, string>
    {
        private IOptions<VideoRestrictsOptions> _videoRestictsOptions;

        public YoutubeVideoGetter(IOptions<VideoRestrictsOptions> videoRestictsOptions)
        {
            _videoRestictsOptions = videoRestictsOptions;
        }

        public bool AllowedSize(VideoInfo videoInfo)
        {
            if (videoInfo is null)
                throw new ArgumentNullException(nameof(videoInfo));

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(videoInfo.DownloadUrl);
            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
            
            string contentLenghtHeaderValue = webResponse.Headers["Content-Length"];

            webResponse.Close();
            if (string.IsNullOrEmpty(contentLenghtHeaderValue))
                return false;            

            return int.TryParse(contentLenghtHeaderValue, out int contentLenght) && contentLenght <= _videoRestictsOptions.Value.AllowedSize;
        }

        public VideoInfo GetVideoInfo(string resource)
        {
            if (resource is null)
                throw new ArgumentNullException(nameof(resource));

            IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(resource);
            return videoInfos.FirstOrDefault(info => info.VideoType == VideoType.Mp4 && info.Resolution == 360);
        }
    }
}
