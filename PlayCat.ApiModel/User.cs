using PlayCat.DataModel;
using System;

namespace PlayCat.ApiModel
{
    public class User
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string NickName { get; set; }   
        
        public string Email { get; set; }

        public DateTime RegisterDate { get; set; }

        public bool IsUploading { get; set; }
    }
}
