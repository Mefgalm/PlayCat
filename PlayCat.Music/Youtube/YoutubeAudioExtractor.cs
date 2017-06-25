using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExtractor;

namespace PlayCat.Music.Youtube
{
    public class YoutubeAudioExtractor : AudioExtractor<VideoInfo, string, VideoFileOnFS, AudioFileOnFS>
    {        
        public YoutubeAudioExtractor(
            IVideoGetter<VideoInfo, string> videoGetter,
            ISaveVideo<VideoInfo, VideoFileOnFS> saveVideo
            ) 
            : base(videoGetter, saveVideo, null, null)
        {
        }

        public override AudioFileOnFS SaveAudio(VideoFileOnFS saveVideoInfo)
        {            
            throw new NotImplementedException();
        }

        public override VideoFileOnFS SaveVideo(string resource)
        {
            throw new NotImplementedException();
        }
    }
}
