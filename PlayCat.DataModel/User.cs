using PlayCat.DataModel.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace PlayCat.DataModel
{
    public class User : IValidationUser
    {
        //validation block - should be sync with ApiModel.User
        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        [MaxLength(100)]
        public string NickName { get; set; }
        //end of validation block

        [Key]
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string PasswordHash { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string PasswordSald { get; set; }

        public DateTime RegisterDate { get; set; }

        public Guid VerificationCode { get; set; }

        public bool IsUploading { get; set; }        
    }
}
