using PlayCat.DataModel;
using PlayCat.DataModel.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace PlayCat.ApiModel
{
    public class User : IValidationUser
    {
        //validation block - should be sync with DataModel.User
        [RegularExpression("^[a-zA-Z0-9_]{3,100}$", ErrorMessage = "FirstName allowed symbols A-Z, _ in range 3 to 100")]
        public string FirstName { get; set; }

        [RegularExpression("^[a-zA-Z0-9_]{3,100}$", ErrorMessage = "LastName allowed symbols A-Z, _ in range 3 to 100")]
        public string LastName { get; set; }

        [RegularExpression("^[a-zA-Z0-9_]{3,100}$", ErrorMessage = "NickName allowed symbols A-Z, _ in range 3 to 100")]
        public string NickName { get; set; }
        //end of validation block

        public Guid Id { get; set; }

        public DateTime RegisterDate { get; set; }

        public bool IsUploading { get; set; }

        public virtual AuthToken AuthToken { get; set; }
    }
}
