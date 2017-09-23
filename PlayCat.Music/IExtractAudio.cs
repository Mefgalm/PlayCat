using System.Threading.Tasks;

namespace PlayCat.Music
{
    public interface IExtractAudio
    {
        Task<IFile> ExtractAsync(IFile videoFile);
    }
}
