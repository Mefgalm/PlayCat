using Newtonsoft.Json;
using PlayCat.DataService.Attributes.RegexValidation;
using System.ComponentModel.DataAnnotations;

namespace PlayCat.DataService.Request
{
    public class SignUpRequest
    {
        [RegexValidation("^[a-zA-Z0-9_]{3,100}$", "FirstName allowed symbols A-Z, _ in range 3 to 100")]
        public string FirstName { get; set; }

        [RegexValidation("^[a-zA-Z0-9_]{3,100}$", "FirstName allowed symbols A-Z, _ in range 3 to 100")]
        public string LastName { get; set; }
        
        [RegexValidation(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", "Email is invalid")]
        public string Email { get; set; }

        [RegexValidation(@"^(?=.*[a-z])(?=.*\d)[a-zA-Z0-9]{3,16}$", "Password must be in range 3 to 16 with characters and numbers")]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Passwords don't match.")]
        public string ConfirmPassword { get; set; }

        [RegexValidation(@"^[a-zA-Z0-9\-]+$", "Verification code must can't be empty")]
        public string VerificationCode { get; set; }
    }
}
