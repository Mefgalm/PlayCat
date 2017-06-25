namespace PlayCat.Music
{
    public interface IUploadAudio<T, K>
    {
        K UploadAudio(T audioInfo);
    }
}
