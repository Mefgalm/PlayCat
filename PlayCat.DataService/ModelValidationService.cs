using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using PlayCat.Helpers;

namespace PlayCat.DataService
{
    public class ModelValidationService : IModelValidationService
    {
        public const string Pattern = "pattern";
        public const string Required = "required";
        public const string Compare = "compare";

        public string AssemblyName { get; set; } = "PlayCat.DataService.Request.";

        public IDictionary<string, IDictionary<string, ValidationModel>> GetModel(string typeName)
        {
            Assembly assembly = GetType().Assembly;
            BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance;

            var modelValidationDictionary = new Dictionary<string, IDictionary<string, ValidationModel>>();
            try
            {
                Type type = assembly.GetType(AssemblyName + typeName);

                if (type == null)
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

        private IDictionary<string, ValidationModel> GetMappedAttributes(string propName, IEnumerable<ValidationAttribute> validationAttributes)
        {
            if (validationAttributes == null)
                throw new ArgumentNullException(nameof(validationAttributes));

            var attrDictionary = new Dictionary<string, ValidationModel>();
            foreach (var attr in validationAttributes)
            {
                var pair = GetValidationKey(propName, attr);
                if (pair.errorMessage != null && pair.validationRule != null)
                {
                    attrDictionary.Add(pair.validationRule, new ValidationModel()
                    {
                        ErrorMessage = pair.errorMessage,
                        ValidationValue = pair.validationValue,
                    });
                }
            }

            return attrDictionary;
        }

        private (string validationRule, string errorMessage, string validationValue) GetValidationKey(string propName, ValidationAttribute validationAttribute)
        {
            string errorMessage = validationAttribute.ErrorMessage ?? validationAttribute.FormatErrorMessage(propName);
            if (validationAttribute is RegularExpressionAttribute reg)
            {
                return (Pattern, errorMessage, reg.Pattern);
            }
            if (validationAttribute is RequiredAttribute)
            {
                return (Required, errorMessage, null);
            }
            if(validationAttribute is CompareAttribute com)
            {
                return (Compare, errorMessage, com.OtherProperty.ToLowerFirstCharacter());
            }

            return (null, null, null);
        }
    }
}   
