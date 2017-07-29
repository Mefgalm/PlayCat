
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayCat.DataModel
{
    public class Audio
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(50)]
        [Required(AllowEmptyStrings = false)]
        public string Artist { get; set; }

        [MaxLength(50)]
        [Required(AllowEmptyStrings = false)]        
        public string Song { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string AccessUrl { get; set; }        

        [Required(AllowEmptyStrings = false)]
        public string FileName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Extension { get; set; }

        public DateTime DateCreated { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string UniqueIdentifier { get; set; }

        public Guid? UploaderId { get; set; }

        [ForeignKey(nameof(UploaderId))]
        public virtual User Uploader { get; set; }
    }
}
