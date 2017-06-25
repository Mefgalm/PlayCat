namespace PlayCat.Music
{
    public abstract class AudioExtractor<TVideoInfo, TResource, TVideoFile, TAudioFile>
        : IAudioExtractor<TVideoInfo, TResource, TVideoFile, TAudioFile>
        where TVideoFile : IFile, IVideoFile
        where TAudioFile : IFile, IAudioFile
    {
        private IVideoGetter<TVideoInfo, TResource> _videoGetter;
        private ISaveVideo<TVideoInfo, TVideoFile> _extractVideo;
        private IExtractAudio<TVideoFile, TAudioFile> _extractAudio;
        private IUploadAudio<TAudioFile, TAudioFile> _uploadAudio;      
        
        public AudioExtractor(
            IVideoGetter<TVideoInfo, TResource> videoGetter,
            ISaveVideo<TVideoInfo, TVideoFile> extractVideo,
            IExtractAudio<TVideoFile, TAudioFile> extractAudio,
            IUploadAudio<TAudioFile, TAudioFile> uploadAudio)
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
        public IUploadAudio<TAudioFile, TAudioFile> UploadAudio
        {
            get => _uploadAudio;
            set => _uploadAudio = value;
        }

        public abstract TVideoFile SaveVideo(TResource resource);
        public abstract TAudioFile SaveAudio(TVideoFile saveVideoInfo);
    }
}
