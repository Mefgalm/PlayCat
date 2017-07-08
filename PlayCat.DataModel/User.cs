using System;
using System.ComponentModel.DataAnnotations;

namespace PlayCat.DataModel
{
    public class User
    {
        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        [MaxLength(100)]
        public string NickName { get; set; }

        [Key]
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string PasswordHash { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string PasswordSald { get; set; }

        public DateTime RegisterDate { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string VerificationCode { get; set; }

        public bool IsUploading { get; set; }  
        
        public virtual AuthToken AuthToken { get; set; }
    }
}
