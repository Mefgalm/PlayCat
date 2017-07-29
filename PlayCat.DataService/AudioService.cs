using Microsoft.Extensions.Options;
using PlayCat.DataService.Request;
using PlayCat.DataService.Response;
using PlayCat.Helpers;
using System.Linq;
using PlayCat.Music;
using System;

namespace PlayCat.DataService
{
    public class AudioService : BaseService, IAudioService
    {        
        private readonly IOptions<VideoRestrictsOptions> _videoRestrictsOptions;

        private readonly IVideoInfoGetter _videoInfoGetter;
        private readonly ISaveVideo _saveVideo;
        private readonly IExtractAudio _extractAudio;
        private readonly IUploadAudio _uploadAudio;

        public AudioService(PlayCatDbContext dbContext, 
            IOptions<VideoRestrictsOptions> videoRestrictsOptions,
            IVideoInfoGetter videoInfoGetter,
            ISaveVideo saveVideo,            
            IExtractAudio extractAudio,
            IUploadAudio uploadAudio)
            : base(dbContext)
        {
            _videoRestrictsOptions = videoRestrictsOptions;
            _videoInfoGetter = videoInfoGetter;
            _saveVideo = saveVideo;
            _extractAudio = extractAudio;
            _uploadAudio = uploadAudio;
        }        

        private TReturn RequestTemplate<TReturn, TRequest>(TRequest request, Func<TRequest, TReturn> func)
            where TReturn : BaseResult, new()
        {
            try
            {
                TrimStrings.Trim(request);

                ModelValidationResult modelValidationResult = ModelValidator.Validate(request);
                if (!modelValidationResult.Ok)
                    return ResponseFactory.With(new TReturn()
                    {
                        Errors = modelValidationResult.Errors
                    })
                    .HideInfo()
                    .Fail("Model is not valid");

                return func(request);
            }
            catch (Exception ex)
            {
                return ResponseFactory.With<TReturn>().Fail(ex.Message);
            }
        }

        public GetInfoResult GetInfo(UrlRequest request)
        {
            return RequestTemplate(request, (req) =>
            {
                IUrlInfo urlInfo = _videoInfoGetter.GetInfo(req.Url);

                //urlInfo can't be null
                if (urlInfo is null)
                    throw new ArgumentNullException(nameof(urlInfo));
                
                if(urlInfo.ContentLenght > _videoRestrictsOptions.Value.AllowedSize)
                    return ResponseFactory.With<GetInfoResult>().Fail("Maximim video size is 25 MB");

                return ResponseFactory.With(new GetInfoResult()
                {
                    UrlInfo = urlInfo,
                }).Success();
            });              
        }

        public BaseResult UploadAudio(UploadAudioRequest request)
        {
            return RequestTemplate(request, (req) =>
            {
                GetInfoResult result = GetInfo(new UrlRequest() { Url = req.Url });

                if (!result.Ok)
                    return ResponseFactory.With<BaseResult>().Fail(result.Info, result.Errors);

                string videoId = UrlFormatter.GetYoutubeVideoIdentifier(req.Url);

                if (_dbContext.Audios.Any(x => x.UniqueIdentifier == videoId))
                    return ResponseFactory.With<BaseResult>().Fail("Video already uploaded");

                IFile videoFile = _saveVideo.Save(req.Url);
                IFile audioFile = _extractAudio.Extract(videoFile);

                string accessUrl = _uploadAudio.Upload(audioFile, StorageType.FileSystem); //TODO create upload for FileSystem, Blob, etc...

                _dbContext.Audios.Add(new DataModel.Audio()
                {
                    Id = Guid.NewGuid(),
                    AccessUrl = accessUrl,
                    DateCreated = DateTime.Now,
                    Artist = req.Artist,
                    Song = req.Song,
                    Extension = audioFile.Extension,
                    FileName = audioFile.Filename,
                    UniqueIdentifier = videoId,
                    //UploaderId
                });

                return ResponseFactory.With<BaseResult>().Success();
            });
        }        
    }
}
