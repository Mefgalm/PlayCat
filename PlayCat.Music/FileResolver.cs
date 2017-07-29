using Microsoft.Extensions.Options;
using System;
using System.IO;
using PlayCat.Helpers;

namespace PlayCat.Music
{
    public class FileResolver : IFileResolver
    {
        private readonly IOptions<FolderOptions> _folderOptions;

        public FileResolver(IOptions<FolderOptions> folderPathService)
        {
            _folderOptions = folderPathService;
        }

        public string AudioFilePath(string filename, string extension, StorageType storageType)
        {
            if (filename is null)
                throw new ArgumentNullException(nameof(filename));

            switch(storageType)
            {
                case StorageType.FileSystem:
                    return Path.Combine(_folderOptions.Value.VideoFolderPath, filename.AddExtension(extension));
            }

            throw new MissingStorageTypeException();
        }        

        public string VideoFilePath(string filename, string extension, StorageType storageType)
        {
            if (filename is null)
                throw new ArgumentNullException(nameof(filename));

            switch (storageType)
            {
                case StorageType.FileSystem:
                    return Path.Combine(_folderOptions.Value.VideoFolderPath, filename.AddExtension(extension));
            }

            throw new MissingStorageTypeException();
        }

        public string GetAudioFolderPath(StorageType storageType)
        {
            switch (storageType)
            {
                case StorageType.FileSystem:
                    return _folderOptions.Value.AudioFolderPath;
            }

            throw new MissingStorageTypeException();
        }

        public string GetVideoFolderPath(StorageType storageType)
        {
            switch (storageType)
            {
                case StorageType.FileSystem:
                    return _folderOptions.Value.VideoFolderPath;
            }

            throw new MissingStorageTypeException();
        }
    }
}
