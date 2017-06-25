using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayCat.Music
{
    public interface IAudioExtractor<TVideoInfo, TResource, TVideoFile, TAudioFile>
        where TVideoFile : IFile, IVideoFile
        where TAudioFile : IFile, IAudioFile
    {
        IVideoGetter<TVideoInfo, TResource> VideoGetter { get; set; }
        ISaveVideo<TVideoInfo, TVideoFile> ExtractVideo { get; set; }
        IExtractAudio<TVideoFile, TAudioFile> ExtractAudio { get; set; }
        IUploadAudio<TAudioFile, TAudioFile> UploadAudio { get; set; }

        TVideoFile SaveVideo(TResource resource);
        TAudioFile SaveAudio(TVideoFile saveVideoInfo);
    }
}
