using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PlayCat.DataService.Request.AudioRequest;
using PlayCat.DataService.Response;
using PlayCat.DataService.Response.AudioRequest;
using PlayCat.Helpers;
using PlayCat.Music;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayCat.DataService
{
    public class UploadService : BaseService, IUploadService
    {
        private readonly IVideoInfoGetter _videoInfoGetter;
        private readonly ISaveVideo _saveVideo;
        private readonly IExtractAudio _extractAudio;
        private readonly IUploadAudio _uploadAudio;

        private readonly IOptions<VideoRestrictsOptions> _videoRestrictsOptions;

        public UploadService(
            PlayCatDbContext dbContext,
            IOptions<VideoRestrictsOptions> videoRestrictsOptions,
            IVideoInfoGetter videoInfoGetter,
            ISaveVideo saveVideo,
            IExtractAudio extractAudio,
            IUploadAudio uploadAudio,
            ILoggerFactory loggerFactory)
            : base(dbContext, loggerFactory.CreateLogger<UploadService>())
        {
            _videoInfoGetter = videoInfoGetter;
            _saveVideo = saveVideo;
            _extractAudio = extractAudio;
            _uploadAudio = uploadAudio;

            _videoRestrictsOptions = videoRestrictsOptions;
        }

        public GetInfoResult GetInfo(UrlRequest request)
        {
            return RequestTemplateCheckModel(request, () =>
            {
                IUrlInfo urlInfo = _videoInfoGetter.GetInfo(request.Url);

                //urlInfo can't be null
                if (urlInfo == null)
                    throw new ArgumentNullException(nameof(urlInfo));

                if (urlInfo.ContentLenght > _videoRestrictsOptions.Value.AllowedSize)
                    return ResponseBuilder<GetInfoResult>.Create().Fail().SetInfoAndBuild("Maximim video size is 25 MB");

                return ResponseBuilder<GetInfoResult>.SuccessBuild(new GetInfoResult()
                {
                    UrlInfo = urlInfo,
                });
            });
        }

        public BaseResult UploadAudio(Guid userId, UploadAudioRequest request)
        {
            return RequestTemplateCheckModel(request, () =>
            {
                var responseBuilder =
                    ResponseBuilder<BaseResult>
                    .Create()
                    .Fail();

                GetInfoResult result = GetInfo(new UrlRequest() { Url = request.Url });

                if (!result.Ok)
                    return responseBuilder.SetErrors(result.Errors).SetInfoAndBuild(result.Info);

                string videoId = UrlFormatter.GetYoutubeVideoIdentifier(request.Url);

                if (_dbContext.Audios.Any(x => x.UniqueIdentifier == videoId))
                    return responseBuilder.SetInfoAndBuild("Video already uploaded");

                IFile videoFile = _saveVideo.Save(request.Url);
                IFile audioFile = _extractAudio.Extract(videoFile);

                //TODO: create upload for FileSystem, Blob, etc...
                string accessUrl = _uploadAudio.Upload(audioFile, StorageType.FileSystem);

                var generalPlayList = _dbContext.Playlists.FirstOrDefault(x => x.OwnerId == userId && x.IsGeneral);

                var audio = new DataModel.Audio()
                {
                    Id = Guid.NewGuid(),
                    AccessUrl = accessUrl,
                    DateCreated = DateTime.Now,
                    Artist = request.Artist,
                    Song = request.Song,
                    Extension = audioFile.Extension,
                    FileName = audioFile.Filename,
                    UniqueIdentifier = videoId,
                    UploaderId = userId,
                };

                var audioPlaylist = new DataModel.AudioPlaylist()
                {
                    AudioId = audio.Id,
                    DateCreated = DateTime.Now,
                    PlaylistId = generalPlayList.Id,
                };

                _dbContext.AudioPlaylists.Add(audioPlaylist);
                _dbContext.Audios.Add(audio);

                _dbContext.SaveChanges();

                return ResponseBuilder<BaseResult>.SuccessBuild();
            });
        } 
    }
}
