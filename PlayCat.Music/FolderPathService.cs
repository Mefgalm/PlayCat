using System;
using Microsoft.Extensions.Options;

namespace PlayCat.Music
{
    public class FolderPathService : IFolderPathService
    {
        public IOptions<FolderOptions> _folderOptions;

        public FolderPathService(IOptions<FolderOptions> folderOptions)
        {
            _folderOptions = folderOptions;
        }

        string IFolderPathService.VideoFolderPath => _folderOptions.Value.VideoFolderPath;

        string IFolderPathService.AudioFolderPath => _folderOptions.Value.AudioFolderPath;

        string IFolderPathService.RelativeAudioFolderPath => _folderOptions.Value.RelativeAudioFolderPath;
    }
}
