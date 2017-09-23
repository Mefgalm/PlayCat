using Microsoft.Extensions.Options;
using NReco.VideoConverter;
using System;
using System.IO;
using PlayCat.Helpers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;

namespace PlayCat.Music
{
    public class FFmpegExtractAudio : IExtractAudio
    {
        //Exampe: ffmpeg -i video.mp4 -f mp3 -ab 192000 -vn music.mp3

        //The -i option in the above command is simple: it is the path to the input file. 
        //The second option -f mp3 tells ffmpeg that the ouput is in mp3 format. 
        //The third option i.e -ab 192000 tells ffmpeg that we want the output 
        //    to be encoded at 192Kbps and -vn tells ffmpeg that we dont want video. 
        //The last param is the name of the output file.

        private const string FFMpegExtractAudioFormat = "-i \"{0}\" -f {1} -ab {2} -vn \"{3}\"";
        private const string FFMegDurationFromat = "-i \"{0}\" 2>&1 | find \"Duration\"";

        private readonly Regex FindDurationRegex = new Regex(@"Duration: (\d+):(\d+):(\d+).(\d+)");

        private readonly IOptions<AudioOptions> _audioOptions;
        private readonly IFileResolver _fileResolver;

        private double _duration = 0;
        private readonly CancellationTokenSource _cancellationTokenSource;

        public FFmpegExtractAudio(IOptions<AudioOptions> audioOptions, IFileResolver fileResolver)
        {
            _audioOptions = audioOptions;
            _fileResolver = fileResolver;
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public async Task<IFile> ExtractAsync(IFile videoFile)
        {
            if (videoFile == null)
                throw new ArgumentNullException(nameof(videoFile));

            if (videoFile.StorageType != StorageType.FileSystem)
                throw new Exception("FFMpeg can work only with file on FileSystem");

            //get full path for audio
            string audioFullpath = Path.Combine(
                _fileResolver.GetAudioFolderPath(StorageType.FileSystem),
                videoFile.Filename + "." + _audioOptions.Value.DefaultFormat);

            //get video
            string videofilePath = _fileResolver.VideoFilePath(videoFile.Filename, videoFile.Extension, StorageType.FileSystem);

            if (File.Exists(audioFullpath))
                File.Delete(audioFullpath);

            //extract audio from video
            var ffMpeg = new FFMpegConverter();

            ffMpeg.LogReceived += FfMpeg_LogReceived;

            ffMpeg.Invoke(
                string.Format(FFMpegExtractAudioFormat,
                                    videofilePath,
                                    _audioOptions.Value.DefaultFormat,
                                    _audioOptions.Value.BitRate,
                                    audioFullpath));

            var cancellationTokenSource = new CancellationTokenSource();
            
            //delete video
            File.Delete(videofilePath);

            try
            {
                await Task.Delay(10000, _cancellationTokenSource.Token);
            } catch (TaskCanceledException)
            {
                //do nothing cancel it okey
            }
            
            //return info about audio file
            return new PCFile()
            {                
                Filename = videoFile.Filename,    
                Extension = "." + _audioOptions.Value.DefaultFormat,     
                Duration = _duration,
                StorageType = StorageType.FileSystem,
            };
        }

        private void FfMpeg_LogReceived(object sender, FFMpegLogEventArgs e)
        {
            Match match = FindDurationRegex.Match(e.Data);
            if(match.Success)
            {
                _duration = int.Parse(match.Groups[1].Value) * 3600 +
                            int.Parse(match.Groups[2].Value) * 60 +
                            int.Parse(match.Groups[3].Value) +
                            int.Parse(match.Groups[4].Value) / 100.0;

                _cancellationTokenSource.Cancel();
            }
        }
    }
}
