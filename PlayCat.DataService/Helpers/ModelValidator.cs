using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PlayCat.DataService.Helpers
{
    public class ModelValidator
    {
        public static ModelValidationResult Validate(object obj)
        {
            var errorList = new List<ValidationResult>();
            if(!Validator.TryValidateObject(obj, new ValidationContext(obj), errorList, true))
            {
                return new ModelValidationResult()
                {
                    Ok = false,
                    Errors = errorList.ToDictionary(x => x.MemberNames.FirstOrDefault(), y => y.ErrorMessage),
                };                
            }
            return new ModelValidationResult()
            {
                Ok = true,                
            };
        }
    }
}
