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

        public string AudioFolderPath()
        {
            return _folderOptions.Value.AudioFolderPath;
        }

        public string VideoFolderPath()
        {
            return _folderOptions.Value.VideoFolderPath;
        }
    }
}
