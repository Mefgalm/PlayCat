namespace PlayCat.Music
{
    public interface IUploadFile : IFile
    {
        string AccessUrl { get; set; }
        string PhysicUrl { get; set; }
    }
}
