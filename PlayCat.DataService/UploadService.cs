﻿using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PlayCat.DataService.Mappers;
using PlayCat.DataService.Request;
using PlayCat.DataService.Response;
using PlayCat.DataService.Response.UploadResponse;
using PlayCat.Helpers;
using PlayCat.Music;
using System;
using System.Linq;
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
            return BaseInvokeCheckModel(request, () =>
            {
                IUrlInfo urlInfo = _videoInfoGetter.GetInfo(request.Url);

                var responseBuilder =
                    ResponseBuilder<GetInfoResult>
                    .Fail();

                //urlInfo can't be null
                if (urlInfo == null)
                    throw new ArgumentNullException(nameof(urlInfo));

                if (urlInfo.ContentLength > _videoRestrictsOptions.Value.AllowedSize)
                    return ResponseBuilder<GetInfoResult>.Fail().SetInfoAndBuild("Maximim video size is 25 MB");                

                if (_dbContext.Audios.Any(x => x.UniqueIdentifier == urlInfo.VideoId))
                    return responseBuilder.SetInfoAndBuild("Video already uploaded");

                return ResponseBuilder<GetInfoResult>.SuccessBuild(new GetInfoResult()
                {
                    UrlInfo = urlInfo,
                });
            });
        }

        public async Task<UploadResult> UploadAudioAsync(Guid userId, UploadAudioRequest request)
        {
            return await BaseInvokeCheckModelAsync(request, async () =>
            {
                DataModel.User user = _dbContext.Users.FirstOrDefault(x => x.Id == userId);
                if (user == null)
                    throw new Exception("User not found, but token does");

                var responseBuilder =
                    ResponseBuilder<UploadResult>
                           .Fail();

                if (user.IsUploadingAudio)
                    return responseBuilder.SetInfoAndBuild("User already uploading audio");

                user.IsUploadingAudio = true;
                _dbContext.SaveChanges();

                using (var transaction = _dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        GetInfoResult result = GetInfo(new UrlRequest() { Url = request.Url });

                        if (!result.Ok)
                            return responseBuilder
                                   .SetErrors(result.Errors)
                                   .SetInfoAndBuild(result.Info);

                        string videoId = UrlFormatter.GetYoutubeVideoIdentifier(request.Url);

                        IFile videoFile = _saveVideo.Save(request.Url);
                        IFile audioFile = await _extractAudio.ExtractAsync(videoFile);

                        //TODO: create upload for FileSystem, Blob, etc...
                        string accessUrl = _uploadAudio.Upload(audioFile, StorageType.FileSystem);

                        var generalPlayList = _dbContext.Playlists.FirstOrDefault(x => x.OwnerId == userId && x.IsGeneral);

                        if (generalPlayList == null)
                            throw new Exception("Playlist not found");

                        var audio = new DataModel.Audio()
                        {
                            Id = Guid.NewGuid(),
                            AccessUrl = accessUrl,
                            DateCreated = DateTime.Now,
                            Artist = request.Artist,
                            Song = request.Song,
                            Duration = audioFile.Duration,
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
                            Order = generalPlayList.OrderValue,
                        };

                        //skip upload process
                        user.IsUploadingAudio = false;

                        //update max index in playlist
                        generalPlayList.OrderValue++;

                        //add entities
                        _dbContext.AudioPlaylists.Add(audioPlaylist);
                        DataModel.Audio audioEntity = _dbContext.Audios.Add(audio).Entity;

                        _dbContext.SaveChanges();

                        transaction.Commit();
                        return ResponseBuilder<UploadResult>.SuccessBuild(new UploadResult()
                        {
                            Audio = AudioMapper.ToApi.FromData(audioEntity),
                        });

                    } catch(Exception ex)
                    {
                        transaction.Rollback();

                        user.IsUploadingAudio = false;
                        _dbContext.SaveChanges();

                        throw ex;
                    }
                }
            });
        } 
    }
}
