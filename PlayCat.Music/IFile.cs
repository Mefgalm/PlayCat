namespace PlayCat.Music
{
    public interface IFile
    {
        string Filename { get; set; }
        string Extension { get; set; }
        StorageType StorageType { get; set; }
    }
}
