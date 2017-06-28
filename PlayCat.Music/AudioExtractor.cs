using System;

namespace PlayCat.Music
{
    public abstract class AudioExtractor<TVideoInfo, TResource, TVideoFile, TAudioFile, TUploadFile>
        : IAudioExtractor<TVideoInfo, TResource, TVideoFile, TAudioFile, TUploadFile>
        where TVideoFile : IFile, IVideoFile
        where TAudioFile : IFile, IAudioFile
        where TUploadFile : IUploadFile
    {
        private IVideoGetter<TVideoInfo, TResource> _videoGetter;
        private ISaveVideo<TVideoInfo, TVideoFile> _extractVideo;
        private IExtractAudio<TVideoFile, TAudioFile> _extractAudio;
        private IUploadAudio<TAudioFile, TUploadFile> _uploadAudio;      
        
        public AudioExtractor(
            IVideoGetter<TVideoInfo, TResource> videoGetter,
            ISaveVideo<TVideoInfo, TVideoFile> extractVideo,
            IExtractAudio<TVideoFile, TAudioFile> extractAudio,
            IUploadAudio<TAudioFile, TUploadFile> uploadAudio)
        {
            _videoGetter = videoGetter;
            _extractVideo = extractVideo;
            _extractAudio = extractAudio;
            _uploadAudio = uploadAudio;
        }

        public IVideoGetter<TVideoInfo, TResource> VideoGetter
        {
            get => _videoGetter;
            set => _videoGetter = value;
        }
        public ISaveVideo<TVideoInfo, TVideoFile> ExtractVideo
        {
            get => _extractVideo;
            set => _extractVideo = value;
        }
        public IExtractAudio<TVideoFile, TAudioFile> ExtractAudio
        {
            get => _extractAudio;
            set => _extractAudio = value;
        }
        public IUploadAudio<TAudioFile, TUploadFile> UploadAudio
        {
            get => _uploadAudio;
            set => _uploadAudio = value;
        }

        public abstract string GetUniqueIdentifierOfVideo(string url);
        public abstract string GetUniqueIdentifierOfVideo(TVideoInfo info);
    }
}
