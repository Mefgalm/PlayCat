namespace PlayCat.Music
{
    public interface IExtractAudio<T, K>
    {
        K ExtractAudio(T videoInfo, int bitRate);
    }
}
