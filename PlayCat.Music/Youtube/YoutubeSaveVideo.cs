using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExtractor;

namespace PlayCat.Music.Youtube
{
    public class YoutubeSaveVideo : ISaveVideo<VideoInfo, VideoFileOnFS>
    {
        private IFolderPathService _folderPathService;

        public YoutubeSaveVideo(IFolderPathService folderPathService)
        {
            _folderPathService = folderPathService;
        }

        public VideoFileOnFS SaveVideo(VideoInfo videoInfo)
        {
            if (videoInfo.RequiresDecryption)
            {
                DownloadUrlResolver.DecryptDownloadUrl(videoInfo);
            }

            string videoFolderPath = _folderPathService.VideoFolderPath();
            string fullPath = Path.Combine(videoFolderPath, videoInfo.Title + videoInfo.VideoExtension);

            var videoDownloader = new VideoDownloader(videoInfo, fullPath);

            // Register the ProgressChanged event and print the current progress
            //videoDownloader.DownloadProgressChanged += (sender, args) => Console.WriteLine(args.ProgressPercentage);

            videoDownloader.Execute();

            return new VideoFileOnFS()
            {
                 DateCreated = DateTime.UtcNow,
                 Extension = videoInfo.VideoExtension,
                 FileName = videoInfo.Title,
                 FolderPath = videoFolderPath,
                 FullPath = fullPath,
            };
        }
    }
}
