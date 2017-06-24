using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExtractor;

namespace PlayCat.Music
{
    public interface ISaveVideo
    {
        bool SaveVideo(VideoInfo videoInfo, string folderPath);
    }
}
