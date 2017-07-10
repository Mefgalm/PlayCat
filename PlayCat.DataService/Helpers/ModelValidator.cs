using PlayCat.DataService.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace PlayCat.DataService.Helpers
{
    public class ModelValidator
    {
        public static ModelValidationResult Validate(object obj)
        {
            IEnumerable<ValidateResult> validationResults = Validator.Validate(obj);
            IEnumerable<ValidateResult> invalidResults = validationResults.Where(x => !x.Ok);
            return new ModelValidationResult()
            {
                Ok = validationResults.All(x => x.Ok),
                Errors = invalidResults.Any() 
                    ? invalidResults.ToDictionary(x => x.FieldResult.FieldName, y => y.FieldResult.ErrorMessages)
                    : null,
            };
        }
    }
}
