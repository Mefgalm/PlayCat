﻿using System.ComponentModel.DataAnnotations;

namespace PlayCat.DataService.Request
{
    public class SignUpRequest
    {
        [RegularExpression("^[a-zA-Z0-9_]{3,100}$", ErrorMessage = "FirstName allowed symbols A-Z, _ in range 3 to 100")]
        public string FirstName { get; set; }

        [RegularExpression("^[a-zA-Z0-9_]{3,100}$", ErrorMessage = "FirstName allowed symbols A-Z, _ in range 3 to 100")]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [RegularExpression("^[a-zA-Z0-9]{3,16}$", ErrorMessage = "Password must be in range 3 to 16 with characters and numbers")]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Passwords don't match.")]
        public string ConfirmPassword { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Verification code must can't be empty")]
        public string VerificationCode { get; set; }
    }
}
