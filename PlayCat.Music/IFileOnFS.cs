using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayCat.Music
{
    public interface IFileOnFS : IFile
    {
        string FolderPath { get; set; }        
        DateTime DateCreated { get; set; }        
    }
}
