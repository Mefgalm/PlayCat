namespace PlayCat.Music
{
    public interface IUrlInfo
    {
        long ContentLength { get; set; }
        string Artist { get; set; }
        string Song { get; set; }
    }    
}
