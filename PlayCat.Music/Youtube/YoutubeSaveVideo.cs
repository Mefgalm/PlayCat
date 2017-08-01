using PlayCat.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using YoutubeExtractor;

namespace PlayCat.Music.Youtube
{
    public class YoutubeSaveVideo : ISaveVideo
    {
        private const string YoutuberRegexp = @"^(http(s)??\:\/\/)?(www\.)?(youtube\.com\/watch\?v=[\.A-Za-z0-9_\?=&-]+|youtu\.be\/[A-Za-z0-9_-]+)$";        

        private readonly IFileResolver _fileResolver;

        public YoutubeSaveVideo(IFileResolver fileResolver)
        {
            _fileResolver = fileResolver;
        }        

        public IFile Save(string url)
        {
            if (url == null)
                throw new ArgumentNullException(nameof(url));

            url = url.Trim();

            if (!new Regex(YoutuberRegexp).Match(url).Success)
                throw new Exception("Link is not valid for youtube video");

            url = UrlFormatter.RemoveParametersFromUrl(url);

            IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(url);
            VideoInfo videoInfo = videoInfos.First(info => info.VideoType == VideoType.Mp4 && info.Resolution == 360);

            if (videoInfo.RequiresDecryption)
            {
                DownloadUrlResolver.DecryptDownloadUrl(videoInfo);
            }

            string videoFolderPath = _fileResolver.GetVideoFolderPath(StorageType.FileSystem);
            string fileName = UrlFormatter.GetYoutubeVideoIdentifier(url);
            string fullPath = Path.Combine(videoFolderPath, fileName + videoInfo.VideoExtension);

            if (File.Exists(fullPath))
                File.Delete(fullPath);

            var videoDownloader = new VideoDownloader(videoInfo, fullPath);
            videoDownloader.Execute();

            return new PCFile()
            {
                Extension = videoInfo.VideoExtension,
                Filename = fileName,
                StorageType = StorageType.FileSystem,
            };
        }        
    }
}
