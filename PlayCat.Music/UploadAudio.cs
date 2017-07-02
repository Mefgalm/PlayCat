using System.IO;

namespace PlayCat.Music.Youtube
{
    public class UploadAudio : IUploadAudio<AudioFileOnFS, UploadFile>
    {
        private IFolderPathService _folderPathService;

        public UploadAudio(IFolderPathService folderPathService)
        {
            _folderPathService = folderPathService;
        }

        public UploadFile Upload(AudioFileOnFS audioInfo)
        {
            return new UploadFile()
            {
                AccessUrl = _folderPathService.RelativeAudioFolderPath + "\\" + audioInfo.FileName + audioInfo.Extension,
                DateCreated = audioInfo.DateCreated,
                Extension = audioInfo.Extension,
                FileName = audioInfo.FileName,
                PhysicUrl = audioInfo.FullPath,
                Artist = audioInfo.Artist,
                Song = audioInfo.Song,
                VideoId = audioInfo.VideoId,
            };
        }
    }
}
