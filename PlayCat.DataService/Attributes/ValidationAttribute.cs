using System;

namespace PlayCat.DataService.Attributes
{
    public abstract class ValidationAttribute : Attribute
    {
        public string FieldName { get; set; }
        public string ErrorMessage { get; set; }

        public abstract string Validate(object obj);
    }
}
