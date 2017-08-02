using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PlayCat.ApiModel
{
    public class Playlist
    {
        //[RegularExpression("^[a-zA-Z0-9_']{1,100}$", ErrorMessage = "Title allowed symbols A-Z, _, ' in range 1 to 100")]
        public string Title { get; set; }

        public bool IsGeneral { get; set; }

        public Guid Id { get; set; }        

        public User Owner { get; set; }

        public IEnumerable<Audio> Audios { get; set; } = Enumerable.Empty<Audio>();
    }
}
