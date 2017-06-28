using System;

namespace PlayCat.Music
{
    public class UploadFile : IUploadFile, IAudioFile
    {
        public string AccessUrl { get; set; }
        public string PhysicUrl { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public DateTime DateCreated { get; set; }
        public string VideoId { get; set; }
        public string Artist { get; set; }
        public string Song { get; set; }
    }
}
