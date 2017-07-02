using PlayCat.DataModel;
using PlayCat.DataModel.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PlayCat.ApiModel
{
    public class Playlist : IValidationPlaylist
    {
        //validation block - should be sync with DataModel.Playlist
        [RegularExpression("^[a-zA-Z0-9_']{1,100}$", ErrorMessage = "Title allowed symbols A-Z, _, ' in range 1 to 100")]
        public string Title { get; set; }
        //end of validation block

        public Guid Id { get; set; }

        public IEnumerable<Audio> Audios { get; set; } = Enumerable.Empty<Audio>();
    }
}
