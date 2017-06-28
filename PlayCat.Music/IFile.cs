using System;

namespace PlayCat.Music
{
    public interface IFile
    {
        string FileName { get; set; }           
        string Extension { get; set; }
        DateTime DateCreated { get; set; }
    }
}
