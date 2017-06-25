using PlayCat.Music;
using YoutubeExtractor;

namespace PlayCat.DataService
{
    public class AudioService
    {
        private readonly PlayCatDbContext _dbContext;
        private readonly IAudioExtractor<VideoInfo, string, VideoFileOnFS, AudioFileOnFS> _audioExtractor;

        public AudioService(PlayCatDbContext dbContext, IAudioExtractor<VideoInfo, string, VideoFileOnFS, AudioFileOnFS> audioExtractor)
        {
            _dbContext = dbContext;
            _audioExtractor = audioExtractor;
        }
    }
}
