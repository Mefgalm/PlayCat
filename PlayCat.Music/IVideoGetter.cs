namespace PlayCat.Music
{
    public interface IVideoGetter<T, K>
    {
        T GetVideoInfo(K resource);
        bool AllowedSize(T videoInfo);
    }
}
