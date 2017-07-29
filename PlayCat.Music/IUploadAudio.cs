namespace PlayCat.Music
{
    public interface IUploadAudio
    {
        string Upload(IFile audioFile, StorageType storageType);
    }
}
