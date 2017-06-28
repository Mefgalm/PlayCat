namespace PlayCat.Music
{
    public interface IFileOnFS : IFile
    {
        string FolderPath { get; set; }
        string FullPath { get; set; }
    }
}
