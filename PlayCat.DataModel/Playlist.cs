using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayCat.DataModel
{
    public class Playlist
    {
        [MaxLength(100)]
        public string Title { get; set; }

        public bool IsGeneral { get; set; }

        [Key]
        public Guid Id { get; set; }        

        public Guid? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
    }
}
