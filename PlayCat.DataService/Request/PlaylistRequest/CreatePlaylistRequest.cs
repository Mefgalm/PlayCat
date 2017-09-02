using System.ComponentModel.DataAnnotations;

namespace PlayCat.DataService.Request
{
    public class CreatePlaylistRequest
    {
        [RegularExpression("^[a-zA-Z0-9_ -]{3,100}$", ErrorMessage = "Title allowed symbols in range 3 to 100")]
        public string Title { get; set; }
    }
}
