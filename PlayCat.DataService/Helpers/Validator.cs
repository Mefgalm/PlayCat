using Newtonsoft.Json;
using PlayCat.DataService.Attributes;
using PlayCat.DataService.Attributes.RegexValidation;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace PlayCat.DataService.Helpers
{
    public static class Validator
    {
        public static IEnumerable<ValidateResult> Validate(object obj)
        {
            if (obj is null)
                throw new ArgumentNullException(nameof(obj));

            BindingFlags bindingFlags = BindingFlags.Instance |
                   BindingFlags.NonPublic |
                   BindingFlags.Public;

            foreach (var prop in obj.GetType().GetProperties(bindingFlags))
            {
                IEnumerable<ValidationAttribute> validationAttributes = prop
                    .GetCustomAttributes(typeof(ValidationAttribute))
                    .Select(x => x as ValidationAttribute);

                if (!validationAttributes.Any()) continue;

                string fieldName = validationAttributes.FirstOrDefault()?.FieldName;

                if (!validationAttributes.All(x => x.FieldName == fieldName))
                    throw new Exception("Field name must be same for all");

                var fieldResult = new FieldResult()
                {
                    FieldName = fieldName ?? LowerFirstCharacter(prop.Name),
                    ErrorMessages = validationAttributes.Select(x => x.Validate(prop.GetValue(obj))).Where(x => !(x is null)),
                };

                yield return new ValidateResult()
                {
                    Ok = !fieldResult.ErrorMessages.Any(),
                    FieldResult = fieldResult,
                };
            }
        }

        private static string LowerFirstCharacter(string str)
        {
            if (str is null)
                return null;

            if (str == string.Empty)
                return string.Empty;

            return str[0].ToString().ToLower() + str.Substring(1);
        }
    }     
}
