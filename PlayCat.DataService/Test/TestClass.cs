using System.ComponentModel.DataAnnotations;

namespace PlayCat.DataService.Test
{
    public class TestClass
    {
        [Required]
        public string Name { get; set; }

        [Required(ErrorMessage = "Wrong name")]
        public string Name2 { get; set; }

        [RegularExpression("^[a-zA-Z0-9]@[a-zA-Z0-9].[a-zA-Z0-9]$")]
        public string Email { get; set; }

        [RegularExpression("^[a-zA-Z0-9]@[a-zA-Z0-9].[a-zA-Z0-9]$", ErrorMessage = "Wrong email")]
        public string Email2 { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z]$")]
        public string Country { get; set; }
    }
}
