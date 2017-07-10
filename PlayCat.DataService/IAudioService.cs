using PlayCat.DataService.Response;

namespace PlayCat.DataService
{
    public interface IAudioService : ISetDbContext
    {
        UploadAudioResult UploadAudio(string youtubeLink);
    }
}