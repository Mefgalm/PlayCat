using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using YoutubeExtractor;

namespace PlayCat.Music
{
    public class VideoConverter
    {
        public static void DownloadYoutube()
        {
            string link = "https://www.youtube.com/watch?v=rDBbaGCCIhk";

            IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(link);

            VideoInfo video = videoInfos
                .First(info => info.VideoType == VideoType.Mp4 && info.Resolution == 360);

            if (video.RequiresDecryption)
            {
                DownloadUrlResolver.DecryptDownloadUrl(video);
            }

            var videoDownloader = new VideoDownloader(video, Path.Combine("D:/Downloads", video.Title + video.VideoExtension));

            // Register the ProgressChanged event and print the current progress
            videoDownloader.DownloadProgressChanged += (sender, args) => Console.WriteLine(args.ProgressPercentage);

            videoDownloader.Execute();
        }

        public static void Test()
        {
            DownloadYoutube();

            var ffMpeg = new NReco.VideoConverter.FFMpegConverter();

            ffMpeg.Invoke(@"-i C:\Users\Unicreo\Downloads\ScattleBloodline.mp4 -f mp3 -ab 192000 -vn C:\Users\Unicreo\Downloads\video.mp3");
        }
    }
}
