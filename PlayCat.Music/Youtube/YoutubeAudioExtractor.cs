using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExtractor;

namespace PlayCat.Music.Youtube
{
    public class YoutubeAudioExtractor : AudioExtractor<VideoInfo, string, VideoFileOnFS, AudioFileOnFS, UploadFile>
    {        
        public YoutubeAudioExtractor(
            IVideoGetter<VideoInfo, string> videoGetter,
            ISaveVideo<VideoInfo, VideoFileOnFS> saveVideo,
            IFFmpeg<VideoFileOnFS, AudioFileOnFS> extractAudio,
            IUploadAudio<AudioFileOnFS, UploadFile> uploadAudio) 
            : base(videoGetter, saveVideo, extractAudio, uploadAudio)
        {
        }

        public override string GetYoutubeUniqueIdentifierOfVideo(string url)
        {
            if (url is null)
                throw new ArgumentNullException(nameof(url));

            int index = url.LastIndexOf('=');
            if (index < 0)
                throw new Exception("Wrong youtube url link format");

            return url.Substring(index + 1);
        }
    }
}
