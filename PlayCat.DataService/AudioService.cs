using Microsoft.Extensions.Options;
using PlayCat.DataService.Helpers;
using PlayCat.DataService.Response;
using PlayCat.Music;
using System;
using System.Linq;
using System.Net;
using YoutubeExtractor;

namespace PlayCat.DataService
{
    public class AudioService : BaseService, IAudioService
    {        
        private readonly IOptions<VideoRestrictsOptions> _videoRestrictsOptions;
        private readonly IAudioExtractor<VideoInfo, string, VideoFileOnFS, AudioFileOnFS, UploadFile> _audioExtractor;

        private const int DefaulBitrate = 320000;

        public AudioService(PlayCatDbContext dbContext, IOptions<VideoRestrictsOptions> videoRestrictsOptions, IAudioExtractor<VideoInfo, string, VideoFileOnFS, AudioFileOnFS, UploadFile> audioExtractor)
            : base(dbContext)
        {
            _videoRestrictsOptions = videoRestrictsOptions;
            _audioExtractor = audioExtractor;
        }

        public UploadAudioResult UploadAudio(string youtubeLink) 
        {
            try
            {
                VideoInfo videoInfo = _audioExtractor.VideoGetter.GetVideoInfo(youtubeLink);
                if (videoInfo is null)
                    return ResponseFactory.With<UploadAudioResult>().Fail("Video can't be extracted");

                Headers headers = HttpRequester.GetHeaders(videoInfo.DownloadUrl);
                if (headers.ContentLenght > _videoRestrictsOptions.Value.AllowedSize)
                    return ResponseFactory.With<UploadAudioResult>().Fail("Maximim size in 25 MB");

                string uniqueIdentifier = _audioExtractor.GetYoutubeUniqueIdentifierOfVideo(youtubeLink);

                if (_dbContext.Audios.Any(x => x.UniqueIdentifier == uniqueIdentifier))
                    return ResponseFactory.With<UploadAudioResult>().Fail("Video already uploaded");

                VideoFileOnFS videoFileOnFS = _audioExtractor.ExtractVideo.Save(videoInfo, uniqueIdentifier);

                AudioFileOnFS audioFileonFs = _audioExtractor.ExtractAudio.ExtractAudio(videoFileOnFS, DefaulBitrate);

                UploadFile uploadFile = _audioExtractor.UploadAudio.Upload(audioFileonFs);

                _dbContext.Audios.Add(new DataModel.Audio()
                {
                    Id = Guid.NewGuid(),
                    DateCreated = uploadFile.DateCreated,
                    Extension = uploadFile.Extension,
                    FileName = uploadFile.FileName,
                    AccessUrl = uploadFile.AccessUrl,
                    PhysicUrl = uploadFile.PhysicUrl,
                    UniqueIdentifier = uploadFile.VideoId,
                    Artist = uploadFile.Artist,
                    Song = uploadFile.Song,                    
                });

                _dbContext.SaveChanges();

                return ResponseFactory.With(new UploadAudioResult()
                {
                    UploadFile = uploadFile,
                })
                .Success();
            } catch(Exception ex)
            {
                return ResponseFactory.With<UploadAudioResult>().Fail(ex.Message);
            }
        }
    }
}
