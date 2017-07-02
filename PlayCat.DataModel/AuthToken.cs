using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayCat.DataModel
{
    public class AuthToken
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime DateExpired { get; set; }
    
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
    }
}
