namespace PlayCat.Music
{
    public interface IFolderPathService
    {
        string VideoFolderPath { get; }
        string AudioFolderPath { get; }
        string RelativeAudioFolderPath { get; }
    }
}
