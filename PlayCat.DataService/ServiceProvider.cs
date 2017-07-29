using Microsoft.Extensions.DependencyInjection;
using PlayCat.Music;
using PlayCat.Music.Youtube;
using YoutubeExtractor;

namespace PlayCat.DataService
{
    public static class ServiceProvider
    {
        public static void RegisterService(IServiceCollection service)
        {
            service.AddScoped<ISaveVideo, YoutubeSaveVideo>();
            service.AddScoped<IExtractAudio, FFmpegExtractAudio>();
            service.AddScoped<IUploadAudio, UploadAudio>();
            service.AddScoped<IVideoInfoGetter, YoutubeVideoInfoGetter>();
            service.AddScoped<IFileResolver, FileResolver>();
            service.AddScoped<IInviteService, InviteService >();
            service.AddScoped<IAuthService, AuthService>();
            service.AddScoped<IModelValidationService, ModelValidationService>();

            service.AddScoped<IAudioService, AudioService>();
        }
    }
}
