namespace PlayCat.Music
{
    public interface IUrlInfo
    {
        long ContentLenght { get; set; }
        string Artist { get; set; }
        string Song { get; set; }
    }    
}
