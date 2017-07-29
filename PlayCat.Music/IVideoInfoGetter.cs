namespace PlayCat.Music
{
    public interface IVideoInfoGetter
    {
        IUrlInfo GetInfo(string url);
    }
}
