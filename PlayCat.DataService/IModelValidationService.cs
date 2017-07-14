using System.Collections.Generic;

namespace PlayCat.DataService
{
    public interface IModelValidationService
    {
        IDictionary<string, IDictionary<string, string>> GetModel(string typeName);
    }
}
