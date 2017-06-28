namespace PlayCat.Music
{
    public interface IAudioExtractor<TVideoInfo, TResource, TVideoFile, TAudioFile, TUploadFile>
        where TVideoFile : IFile, IVideoFile
        where TAudioFile : IFile, IAudioFile
        where TUploadFile : IUploadFile
    {
        IVideoGetter<TVideoInfo, TResource> VideoGetter { get; set; }
        ISaveVideo<TVideoInfo, TVideoFile> ExtractVideo { get; set; }
        IExtractAudio<TVideoFile, TAudioFile> ExtractAudio { get; set; }
        IUploadAudio<TAudioFile, TUploadFile> UploadAudio { get; set; }

        string GetUniqueIdentifierOfVideo(string url);
        string GetUniqueIdentifierOfVideo(TVideoInfo videoInfo);
    }
}
