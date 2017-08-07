using System.ComponentModel.DataAnnotations;

namespace PlayCat.DataService.Request.AuthRequest
{
    public class SignInRequest
    {
        //[Required]
        //[RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Email is invalid")]
        public string Email { get; set; }

        //[Required]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*\d)[a-zA-Z0-9]{3,16}$", ErrorMessage = "Password must be in range 3 to 16 with characters and numbers")]
        public string Password { get; set; }
    }
}