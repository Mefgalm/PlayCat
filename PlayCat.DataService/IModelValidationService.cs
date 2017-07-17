using System.Collections.Generic;

namespace PlayCat.DataService
{
    public interface IModelValidationService
    {
        string AssemblyName { get; set; }
        IDictionary<string, IDictionary<string, ValidationModel>> GetModel(string typeName);
    }
}
