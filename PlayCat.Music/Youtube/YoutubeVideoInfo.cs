using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayCat.Music.Youtube
{
    public enum AdaptiveType
    {
        None = 0,
        Audio = 1,
        Video = 2
    }

    public enum AudioType
    {
        Aac = 0,
        Mp3 = 1,
        Vorbis = 2,
        Unknown = 3
    }

    public enum VideoType
    {
        Mobile = 0,
        Flash = 1,
        Mp4 = 2,
        WebM = 3,
        Unknown = 4
    }

    public class YoutubeVideoInfo
    {
        //
        // Summary:
        //     Gets an enum indicating whether the format is adaptive or not.
        public AdaptiveType AdaptiveType { get; set; }
        //
        // Summary:
        //     The approximate audio bitrate in kbit/s.
        public int AudioBitrate { get; set; }
        //
        // Summary:
        //     Gets the audio extension.
        public string AudioExtension { get; set; }
        //
        // Summary:
        //     Gets the audio type (encoding).
        public AudioType AudioType { get; set; }
        //
        // Summary:
        //     Gets a value indicating whether the audio of this video can be extracted by YoutubeExtractor.
        public bool CanExtractAudio { get; set; }
        //
        // Summary:
        //     Gets the download URL.
        public string DownloadUrl { get; set; }
        //
        // Summary:
        //     Gets the format code, that is used by YouTube internally to differentiate between
        //     quality profiles.
        public int FormatCode { get; set; }
        public bool Is3D { get; set; }
        //
        // Summary:
        //     Gets a value indicating whether this video info requires a signature decryption
        //     before the download URL can be used. This can be achieved with the YoutubeExtractor.DownloadUrlResolver.DecryptDownloadUrl(YoutubeExtractor.VideoInfo)
        public bool RequiresDecryption { get; set; }
        //
        // Summary:
        //     Gets the resolution of the video.
        public int Resolution { get; set; }
        //
        // Summary:
        //     Gets the video title.
        public string Title { get; set; }
        //
        // Summary:
        //     Gets the video extension.
        public string VideoExtension { get; set; }
        
        public VideoType VideoType { get; set; }
    }
}
