namespace PlayCat.Music
{
    public interface IFileResolver
    {
        string VideoFilePath(string filename, string extension, StorageType storageType);
        string AudioFilePath(string filename, string extension, StorageType storageType);

        string GetAudioFolderPath(StorageType storageType);
        string GetVideoFolderPath(StorageType storageType);
    }
}
