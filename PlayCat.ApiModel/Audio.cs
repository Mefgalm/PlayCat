﻿using PlayCat.DataModel;
using PlayCat.DataModel.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace PlayCat.ApiModel
{
    public class Audio : IValidationAudio
    {
        //validation block - should be sync with DataModel.Audio
        [RegularExpression("^[a-zA-Z0-9_']{3,100}$", ErrorMessage = "Artist allowed symbols A-Z, _, ' in range 3 to 100")]
        public string Artist { get; set; }

        [RegularExpression("^[a-zA-Z0-9_']{3,100}$", ErrorMessage = "Song allowed symbols A-Z, _, ' in range 3 to 100")]
        public string Song { get; set; }
        //end of validation block

        public Guid Id { get; set; }

        public string AccessUrl { get; set; }        

        public DateTime DateCreated { get; set; }

        public User Uploader { get; set; }
    }
}