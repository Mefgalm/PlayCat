using Microsoft.AspNetCore.Mvc;
using PlayCat.DataService;
using System.Collections.Generic;

namespace PlayCat.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ValidationController : Controller
    {
        private readonly IModelValidationService _modelValidationService;

        public ValidationController(IModelValidationService modelValidationService)
        {
            _modelValidationService = modelValidationService;
        }

        [HttpGet("validateRules/{typeName}")]
        public IDictionary<string, IDictionary<string, ValidationModel>> ValidateRules(string typeName)
        {
            return _modelValidationService.GetModel(typeName);
        }
    }
}
