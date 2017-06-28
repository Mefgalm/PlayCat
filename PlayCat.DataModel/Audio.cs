using System;
using System.ComponentModel.DataAnnotations;

namespace PlayCat.DataModel
{
    public class Audio
    {
        [Key]
        public Guid Id { get; set; }
        public string AccessUrl { get; set; }
        public string PhysicUrl { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public DateTime DateCreated { get; set; }
        public string Artist { get; set; }
        public string Song { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string UniqueIdentifier { get; set; }
    }
}
