using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExtractor;

namespace PlayCat.Music.Youtube
{
    public class YoutubeVideoGetter : IVideoGetter<VideoInfo, string>
    {
        public VideoInfo GetVideoInfo(string resource)
        {
            IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(resource);
            return videoInfos.FirstOrDefault(info => info.VideoType == VideoType.Mp4 && info.Resolution == 360);
        }
    }
}
