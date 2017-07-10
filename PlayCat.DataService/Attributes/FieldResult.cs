using Newtonsoft.Json;
using System.Collections.Generic;

namespace PlayCat.DataService.Attributes
{
    public class FieldResult
    {
        [JsonProperty("fieldName")]
        public string FieldName { get; set; }

        [JsonProperty("errorMessages")]
        public IEnumerable<string> ErrorMessages { get; set; }
    }
}