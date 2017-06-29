﻿using PlayCat.DataService.Response;
using PlayCat.Music;
using System;
using System.Linq;
using System.Net;
using YoutubeExtractor;

namespace PlayCat.DataService
{
    public class AudioService
    {
        private readonly PlayCatDbContext _dbContext;
        private readonly IAudioExtractor<VideoInfo, string, VideoFileOnFS, AudioFileOnFS, UploadFile> _audioExtractor;

        public AudioService(PlayCatDbContext dbContext, IAudioExtractor<VideoInfo, string, VideoFileOnFS, AudioFileOnFS, UploadFile> audioExtractor)
        {
            _dbContext = dbContext;
            _audioExtractor = audioExtractor;
        }

        public UploadAudioResult UploadAudio(string youtubeLink) 
        {
            if (string.IsNullOrWhiteSpace(youtubeLink))
                return ResponseFactory.With<UploadAudioResult>().Fail("Link can't be null or white space");

            VideoInfo videoInfo = _audioExtractor.VideoGetter.GetVideoInfo(youtubeLink);
            if (videoInfo is null)
                return ResponseFactory.With<UploadAudioResult>().Fail("Video can't be extracted");

            if (!_audioExtractor.VideoGetter.AllowedSize(videoInfo))
                return ResponseFactory.With<UploadAudioResult>().Fail("Maximim size in 25 MB");

            string uniqueIdentifier = _audioExtractor.GetUniqueIdentifierOfVideo(youtubeLink);

            if (_dbContext.Audios.Any(x => x.UniqueIdentifier == uniqueIdentifier))
                return ResponseFactory.With<UploadAudioResult>().Fail("Video already exists in db");

            VideoFileOnFS videoFileOnFS = _audioExtractor.ExtractVideo.SaveVideo(videoInfo, uniqueIdentifier);

            AudioFileOnFS audioFileonFs = _audioExtractor.ExtractAudio.ExtractAudio(videoFileOnFS, 320000);
            UploadFile uploadFile = _audioExtractor.UploadAudio.UploadAudio(audioFileonFs);

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
        }
    }
}