using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PlayCat.DataService.Extensions;

namespace PlayCat.DataService
{
    public class ModelValidationService : IModelValidationService
    {
        public const string Pattern = "pattern";
        public const string Required = "required";

        public IDictionary<string, IDictionary<string, string>> GetModel(string typeName)
        {
            Assembly assembly = typeName.GetType().Assembly;
            BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance;

            var modelValidationDictionary = new Dictionary<string, IDictionary<string, string>>();
            try
            {
                Type type = assembly.GetType(typeName);
                foreach (var prop in type.GetProperties(bindingFlags))
                {
                    IEnumerable<ValidationAttribute> attributes = prop.GetCustomAttributes<ValidationAttribute>();

                    var attrDictionary = new Dictionary<string, string>();
                    foreach (var attr in attributes)
                    {
                        if(attr is RegularExpressionAttribute)
                        {
                            modelValidationDictionary.Add(prop.Name.ToLowerFirstCharacter(), null);
                            (string te, string t2) test;
                            attrDictionary.Add(Pattern, attr.ErrorMessage);
                        }

                    }
                }
                

                return new Dictionary<string, IDictionary<string, string>>
                {
                    
                };
            } catch
            {
                return null;
            }
        }
    }
}
