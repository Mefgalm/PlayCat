﻿using Microsoft.Extensions.DependencyInjection;
using PlayCat.Music;
using PlayCat.Music.Youtube;
using YoutubeExtractor;

namespace PlayCat.DataService
{
    public static class ServiceProvider
    {
        public static void RegisterService(IServiceCollection service)
        {
            service.AddScoped<IAudioExtractor<VideoInfo, string, VideoFileOnFS, AudioFileOnFS, UploadFile>, YoutubeAudioExtractor>();
            service.AddScoped<IVideoGetter<VideoInfo, string>, YoutubeVideoGetter>();
            service.AddScoped<ISaveVideo<VideoInfo, VideoFileOnFS>, YoutubeSaveVideo>();
            service.AddScoped<IExtractAudio<VideoFileOnFS, AudioFileOnFS>, YoutubeExtractAudio>();
            service.AddScoped<IUploadAudio<AudioFileOnFS, UploadFile>, YoutubeUploadAudio>();
            service.AddScoped<IFolderPathService, FolderPathService>();

            service.AddScoped<AudioService>();
        }
    }
}