using PlayCat.DataService.Response;

namespace PlayCat.DataService
{
    public interface IAudioService
    {
        UploadAudioResult UploadAudio(string youtubeLink);

        void SetDbContext(PlayCatDbContext dbContext);
    }
}