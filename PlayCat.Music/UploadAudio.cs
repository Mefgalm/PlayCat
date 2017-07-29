using System;
using System.IO;
using PlayCat.Helpers;

namespace PlayCat.Music
{
    public class UploadAudio : IUploadAudio
    {
        private readonly IFileResolver _fileResolver;

        public UploadAudio(IFileResolver fileResolver)
        {
            _fileResolver = fileResolver;
        }

        public string Upload(IFile audioFile, StorageType storageType)
        {
            switch(storageType)
            {
                case StorageType.FileSystem:
                    return $"/music/{audioFile.Filename.AddExtension(audioFile.Extension)}/storageType/fileSytem";

                //example
                //case StorageType.Blob:
                //    upload file to Blobl
                //
                //    return "/music/{id}/storageType/blob"
            }

            throw new MissingStorageTypeException();
        }
    }
}
