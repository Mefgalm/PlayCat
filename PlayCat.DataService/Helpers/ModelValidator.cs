﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PlayCat.DataService.Helpers
{
    public class ModelValidator
    {
        public static ModelValidationResult Validate(object obj)
        {
            var errors = new List<ValidationResult>();
            if(Validator.TryValidateObject(obj, new ValidationContext(obj), errors, true))
            {
                return new ModelValidationResult()
                {
                    Ok = true,
                };
            }            
            return new ModelValidationResult()
            {
                Ok = false,
                Errors = errors.ToDictionary(x => x.MemberNames.FirstOrDefault() ?? "", y => y.ErrorMessage),
            };
        }
    }
}
