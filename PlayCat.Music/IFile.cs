using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayCat.Music
{
    public interface IFile
    {
        string FileName { get; set; }
        string FullPath { get; set; }
        string Extension { get; set; }
    }
}
