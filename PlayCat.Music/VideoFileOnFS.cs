using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayCat.Music
{
    public class VideoFileOnFS : IFileOnFS, IVideoFile
    {
        public string FolderPath { get; set; }
        public DateTime DateCreated { get; set; }
        public string FileName { get; set; }
        public string FullPath { get; set; }
        public string Extension { get; set; }
        public string Id { get; set; }     
        public string Title { get; set; }
    }
}
