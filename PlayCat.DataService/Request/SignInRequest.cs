using Newtonsoft.Json;
using PlayCat.DataService.Attributes.RegexValidation;

namespace PlayCat.DataService.Request
{
    public class SignInRequest
    {
        [RegexValidation(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", "Email is invalid")]
        public string Email { get; set; }

        [RegexValidation(@"^(?=.*[a-z])(?=.*\d)[a-zA-Z0-9]{3,16}$", "Password must be in range 3 to 16 with characters and numbers")]
        public string Password { get; set; }
    }
}
