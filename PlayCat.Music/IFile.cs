namespace PlayCat.Music
{
    public interface IFile
    {
        string Filename { get; set; }
        string Extension { get; set; }
        double Duration { get; set; }
        StorageType StorageType { get; set; }
    }
}
