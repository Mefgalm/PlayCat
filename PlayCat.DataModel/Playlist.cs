using System;
using System.Collections.Generic;
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

        public Guid? OwnerId { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public virtual User Owner { get; set; }

        public int OrderValue { get; set; }

        public virtual ICollection<AudioPlaylist> AudioPlaylists { get; set; }
    }
}
