using Microsoft.Extensions.Options;
using NReco.VideoConverter;
using System;
using System.IO;
using System.Net;

namespace PlayCat.Music
{
    public class FFmpegExtractAudio : IFFmpeg<VideoFileOnFS, AudioFileOnFS>
    {
        //ffmpeg -i video.mp4 -f mp3 -ab 192000 -vn music.mp3

        //The -i option in the above command is simple: it is the path to the input file. 
        //The second option -f mp3 tells ffmpeg that the ouput is in mp3 format. 
        //The third option i.e -ab 192000 tells ffmpeg that we want the output 
        //    to be encoded at 192Kbps and -vn tells ffmpeg that we dont want video. 
        //The last param is the name of the output file.

        private const string FFMpegFormat = "-i \"{0}\" -f {1} -ab {2} -vn \"{3}\"";

        private readonly IOptions<AudioOptions> _audioOptions;
        private readonly IFolderPathService _folderPathService;

        public FFmpegExtractAudio(IOptions<AudioOptions> audioOptions, IFolderPathService folderPathService)
        {
            _audioOptions = audioOptions;
            _folderPathService = folderPathService;
        }        

        public AudioFileOnFS ExtractAudio(VideoFileOnFS videoInfo, int bitRate)
        {            
            string folderPath = _folderPathService.AudioFolderPath;
            string filename = videoInfo.FileName;
            string fullPath = Path.Combine(folderPath, filename + "." + _audioOptions.Value.DefaultFormat);

            if(File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

            var ffMpeg = new FFMpegConverter();
            ffMpeg.Invoke(
                string.Format(@FFMpegFormat, 
                                    videoInfo.FullPath, 
                                    _audioOptions.Value.DefaultFormat, 
                                    bitRate, 
                                    fullPath));

            File.Delete(videoInfo.FullPath);

            (string Arist, string Song) artistAndSongName = GetArtistAndSongName(videoInfo.Title);

            return new AudioFileOnFS()
            {
                DateCreated = DateTime.Now,
                Extension = "." + _audioOptions.Value.DefaultFormat,
                FileName = videoInfo.FileName,
                FolderPath = folderPath,
                FullPath = fullPath,
                VideoId = videoInfo.Id,     
                Artist = artistAndSongName.Arist,
                Song = artistAndSongName.Song,
            };
        }

        public (string, string) GetArtistAndSongName(string title)
        {
            if (title is null)
                return (string.Empty, string.Empty);

            string[] artistAndSong = title.Split('-');
            if(artistAndSong.Length >= 2)
            {
                return (artistAndSong[0].Trim(), artistAndSong[1].Trim());
            }

            return (title, string.Empty);
        }
    }
}
