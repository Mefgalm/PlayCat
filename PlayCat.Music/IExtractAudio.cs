namespace PlayCat.Music
{
    public interface IFFmpeg<T, K>
    {
        K ExtractAudio(T videoInfo, int bitRate);
    }
}
