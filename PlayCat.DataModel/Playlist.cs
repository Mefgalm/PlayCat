using PlayCat.DataModel.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayCat.DataModel
{
    public class Playlist : IValidationPlaylist
    {
        //validation block - should be sync with ApiModel.Playlist
        [MaxLength(100)]
        [Required(AllowEmptyStrings = false)]        
        public string Title { get; set; }
        //end of validation block

        [Key]
        public Guid Id { get; set; }        

        public Guid? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
    }
}
