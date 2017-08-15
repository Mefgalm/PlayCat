using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayCat.DataService.Request
{
    public class CreatePlaylistRequest
    {
        [Required]
        [RegularExpression("^[a-zA-Z0-9_ -]{3,100}$", ErrorMessage = "Title allowed symbols in range 3 to 100")]
        public string Title { get; set; }
    }
}
