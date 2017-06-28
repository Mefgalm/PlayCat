using System;
using System.ComponentModel.DataAnnotations;

namespace PlayCat.DataModel
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        public string FirstName { get; set; }
    }
}
