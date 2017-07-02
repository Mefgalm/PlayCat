namespace PlayCat.Music
{
    public interface IUploadAudio<T, K>
    {
        K Upload(T audioInfo);
    }
}
