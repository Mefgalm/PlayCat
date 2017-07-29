using System.Collections.Generic;

namespace PlayCat.Helpers
{
    public class ModelValidationResult
    {
        public bool Ok { get; set; }

        public IDictionary<string, string> Errors { get; set; }
    }
}
