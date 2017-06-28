using PlayCat.Music;

namespace PlayCat.DataService.ReturnTypes
{
    public class UploadAudioResult : BaseResult<UploadAudioResult>
    {
        public UploadFile UploadFile { get; set; }
    }
}
