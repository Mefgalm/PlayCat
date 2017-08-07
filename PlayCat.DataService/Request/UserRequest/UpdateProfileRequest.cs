using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayCat.DataService.Request.UserRequest
{
    public class UpdateProfileRequest
    {
        [Required]
        [RegularExpression("^[a-zA-Z0-9_]{3,100}$", ErrorMessage = "FirstName allowed symbols A-Z, _ in range 3 to 100")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9_]{3,100}$", ErrorMessage = "FirstName allowed symbols A-Z, _ in range 3 to 100")]
        public string LastName { get; set; }

        public string NickName { get; set; }
    }
}
