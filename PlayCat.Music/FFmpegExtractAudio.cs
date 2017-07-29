using Microsoft.Extensions.Options;
using NReco.VideoConverter;
using System;
using System.IO;
using PlayCat.Helpers;

namespace PlayCat.Music
{
    public class FFmpegExtractAudio : IExtractAudio
    {
        //ffmpeg -i video.mp4 -f mp3 -ab 192000 -vn music.mp3

        //The -i option in the above command is simple: it is the path to the input file. 
        //The second option -f mp3 tells ffmpeg that the ouput is in mp3 format. 
        //The third option i.e -ab 192000 tells ffmpeg that we want the output 
        //    to be encoded at 192Kbps and -vn tells ffmpeg that we dont want video. 
        //The last param is the name of the output file.

        private const string FFMpegFormat = "-i \"{0}\" -f {1} -ab {2} -vn \"{3}\"";
        private const long BitRate = 320000;

        private readonly IOptions<AudioOptions> _audioOptions;
        private readonly IFileResolver _fileResolver;
        

        public FFmpegExtractAudio(IOptions<AudioOptions> audioOptions, IFileResolver fileResolver)
        {
            _audioOptions = audioOptions;
            _fileResolver = fileResolver;
        }

        public IFile Extract(IFile videoFile)
        {
            if (videoFile is null)
                throw new ArgumentNullException(nameof(videoFile));

            if (videoFile.StorageType != StorageType.FileSystem)
                throw new Exception("FFMpeg can work only with file on FileSystem");

            //get full path for audio
            string audioFullpath = Path.Combine(
                _fileResolver.GetAudioFolderPath(StorageType.FileSystem),
                videoFile.Filename.AddExtension(videoFile.Extension));

            //get video
            string videofilePath = _fileResolver.VideoFilePath(videoFile.Filename, videoFile.Extension, StorageType.FileSystem);

            //extract audio from video
            var ffMpeg = new FFMpegConverter();
            ffMpeg.Invoke(
                string.Format(@FFMpegFormat,
                                    videofilePath,
                                    _audioOptions.Value.DefaultFormat,
                                    BitRate,
                                    audioFullpath));

            //delete video
            File.Delete(videofilePath);


            //return info about audio file
            return new PCFile()
            {                
                Filename = videoFile.Filename,    
                Extension = _audioOptions.Value.DefaultFormat,
                StorageType = StorageType.FileSystem,
            };
        }                
    }
}
