using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PlayCat.DataService.Attributes.RegexValidation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RegexValidationAttribute : ValidationAttribute
    {
        public string Regex { get; private set; }        

        public RegexValidationAttribute(string regex, string errorMessage)
        {
            if (regex is null)
                throw new ArgumentNullException(nameof(regex));

            Regex = regex;
            ErrorMessage = errorMessage;
        }

        public override string Validate(object obj)
        {
            if (obj is null)
            {
                return "is required";
            }

            string str = obj as string;

            if (str == null)
                throw new Exception("Type can be only string");

            var regex = new Regex(Regex);            
            return regex.Match(str).Success ? null : ErrorMessage;            
        }
    }
}
