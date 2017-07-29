using System;

namespace PlayCat.Music
{
    public class MissingStorageTypeException : Exception
    {
        public MissingStorageTypeException() : base("Not recognize StorageType")
        {
        }       
    }
}
