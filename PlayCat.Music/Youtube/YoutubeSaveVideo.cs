using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

        private string CreateVideoFileName(VideoInfo videoInfo)
        {
            return Guid.NewGuid().ToString();
        }

        public VideoFileOnFS SaveVideo(VideoInfo videoInfo, string videoId)
        {
            if (videoInfo.RequiresDecryption)
            {
                DownloadUrlResolver.DecryptDownloadUrl(videoInfo);
            }

            string videoFolderPath = _folderPathService.VideoFolderPath;
            string fileName = CreateVideoFileName(videoInfo);
            string fullPath = Path.Combine(videoFolderPath, fileName + videoInfo.VideoExtension);

            var videoDownloader = new VideoDownloader(videoInfo, fullPath);
            videoDownloader.Execute();

            return new VideoFileOnFS()
            {
                 DateCreated = DateTime.UtcNow,
                 Extension = videoInfo.VideoExtension,
                 FileName = fileName,
                 FolderPath = videoFolderPath,
                 FullPath = fullPath,
                 Id = videoId,
                 Title = videoInfo.Title,
            };
        }
    }
}
