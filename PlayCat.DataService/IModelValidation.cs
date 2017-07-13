using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayCat.DataService
{   
    public interface IModelValidationService
    {
        IDictionary<string, IDictionary<string, string>> GetModel(string typeName);
    }
}
