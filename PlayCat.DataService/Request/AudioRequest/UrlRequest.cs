using System.ComponentModel.DataAnnotations;

namespace PlayCat.DataService.Request
{
    public class UrlRequest
    {
        [Required]
        [RegularExpression(@"^(http(s)??\:\/\/)?(www\.)?(youtube\.com\/watch\?v=[\.A-Za-z0-9_\?=&-]+|youtu\.be\/[A-Za-z0-9_-]+)$", ErrorMessage = "Invalid Url")]
        public string Url { get; set; }
    }
}
