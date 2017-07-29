using System.ComponentModel.DataAnnotations;

namespace PlayCat.DataService.Request
{
    public class UploadAudioRequest
    {
        [Required]
        [RegularExpression("^[a-zA-Z0-9!@#$%^&*()_]{1,50}$")]
        public string Artist { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9!@#$%^&*()_]{1,50}$")]
        public string Song { get; set; }

        [Required]
        [RegularExpression(@"^(http(s)??\:\/\/)?(www\.)?(youtube\.com\/watch\?v=[\.A-Za-z0-9_\?=&-]+|youtu\.be\/[A-Za-z0-9_-]+)$", ErrorMessage = "Invalid Url")]
        public string Url { get; set; }
    }
}
