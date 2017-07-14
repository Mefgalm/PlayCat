using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using PlayCat.DataService.Extensions;

namespace PlayCat.DataService
{
    public class ModelValidationService : IModelValidationService
    {
        public const string Pattern = "pattern";
        public const string Required = "required";

        private const string AssemblyNamespace = "PlayCat.DataService.Test.";
    
        public IDictionary<string, IDictionary<string, string>> GetModel(string typeName)
        {
            Assembly assembly = GetType().Assembly;
            BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance;

            var modelValidationDictionary = new Dictionary<string, IDictionary<string, string>>();
            try
            {
                Type type = assembly.GetType(AssemblyNamespace + typeName);

                if (type is null)
                    return null;

                foreach (var prop in type.GetProperties(bindingFlags))
                {
                    IEnumerable<ValidationAttribute> attributes = prop.GetCustomAttributes<ValidationAttribute>();
                    string propery = prop.Name.ToLowerFirstCharacter();
                    modelValidationDictionary.Add(propery, GetMappedAttributes(propery, attributes));
                }
                return modelValidationDictionary;
            }
            catch
            {
                return null;
            }
        }

        private IDictionary<string, string> GetMappedAttributes(string propName, IEnumerable<ValidationAttribute> validationAttributes)
        {
            if (validationAttributes is null)
                throw new ArgumentNullException(nameof(validationAttributes));

            var attrDictionary = new Dictionary<string, string>();
            foreach (var attr in validationAttributes)
            {
                var pair = GetValidationKey(propName, attr);
                if (pair.errorMessage != null && pair.validationRule != null)
                {
                    attrDictionary.Add(pair.validationRule, pair.errorMessage);
                }
            }

            return attrDictionary;
        }

        private (string validationRule, string errorMessage) GetValidationKey(string propName, ValidationAttribute validationAttribute)
        {
            string errorMessage = validationAttribute.ErrorMessage ?? validationAttribute.FormatErrorMessage(propName);
            if (validationAttribute is RegularExpressionAttribute)
            {
                return (Pattern, errorMessage);
            }
            if (validationAttribute is RequiredAttribute)
            {
                return (Required, errorMessage);
            }

            return (null, null);
        }
    }
}   
