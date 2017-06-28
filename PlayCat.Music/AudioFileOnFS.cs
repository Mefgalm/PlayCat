using System;

namespace PlayCat.Music
{
    public class AudioFileOnFS : IFileOnFS, IAudioFile
    {
        public string FolderPath { get; set; }
        public DateTime DateCreated { get; set; }
        public string FileName { get; set; }
        public string FullPath { get; set; }
        public string Extension { get; set; }
        public string VideoId { get; set; }
        public string Artist { get; set; }
        public string Song { get; set; }
    }
}
