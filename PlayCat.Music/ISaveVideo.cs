namespace PlayCat.Music
{
    public interface ISaveVideo<T, K>
    {
        K Save(T videoInfo, string videoId);
    }
}
