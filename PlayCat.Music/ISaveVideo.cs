namespace PlayCat.Music
{
    public interface ISaveVideo<T, K>
    {
        K SaveVideo(T videoInfo);
    }
}
