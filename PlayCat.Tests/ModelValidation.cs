using PlayCat.DataService;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PlayCat.Tests
{
    public class ModelValidation : BaseTest
    {
        private const string Required = "required";
        private const string Pattern = "pattern";

        [Fact]
        public void ValidModel()
        {
            var modelValidationService = _server.Host.Services.GetService(typeof(IModelValidationService)) as IModelValidationService;

            IDictionary<string, IDictionary<string, string>> model = modelValidationService.GetModel("TestClass");
            Assert.NotNull(model);
            Assert.True(model.Select(x => x.Key)
                             .Intersect(new List<string> { "name", "name2", "email", "email2", "country" })
                             .Count() == 5);

            Assert.True(model["country"].Count == 2);
            
            Assert.True(model["name"].First().Key == Required);

            Assert.True(model["name2"].First().Key == Required);
            Assert.True(model["name2"].First().Value == "Wrong name");

            Assert.True(model["email"].First().Key == Pattern);
            Assert.True(model["email2"].First().Value == "Wrong email");

            Assert.True(model["country"].Any(x => x.Key == Required));
            Assert.True(model["country"].Any(x => x.Key == Pattern));
        }

        [Fact]
        public void EmptyModel()
        {
            var modelValidationService = _server.Host.Services.GetService(typeof(IModelValidationService)) as IModelValidationService;

            IDictionary<string, IDictionary<string, string>> model = modelValidationService.GetModel("EmptyClass");

            Assert.NotNull(model);
            Assert.Empty(model);
        }

        [Fact]
        public void NotFoundModel()
        {
            var modelValidationService = _server.Host.Services.GetService(typeof(IModelValidationService)) as IModelValidationService;

            IDictionary<string, IDictionary<string, string>> model = modelValidationService.GetModel("123");

            Assert.Null(model);
        }
    }
}
