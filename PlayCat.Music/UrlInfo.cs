namespace PlayCat.Music
{
    public class UrlInfo : IUrlInfo
    {
        public long ContentLength { get; set; }
        public string Artist { get; set; }
        public string Song { get; set; }
    }
}
