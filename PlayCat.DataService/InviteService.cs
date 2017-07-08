using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace PlayCat.DataService
{
    public class InviteService : IInviteService
    {
        private const int DaysExpired = 180;
        private const string Filename = "Code.txt";

        private readonly IHostingEnvironment _env;
        private readonly string _code;

        public InviteService(IHostingEnvironment env)
        {
            _env = env;
            _code = File.ReadAllText(Path.Combine(_env.ContentRootPath, Filename));
        }

        public string GenerateInvite()
        {
            var createKey = new SKGL.Generate()
            {
                secretPhase = _code
            };

            return createKey.doKey(DaysExpired);
        }

        //TODO: need return complex model with date?
        public bool IsInviteValid(string key)
        {
            var validation = new SKGL.Validate()
            {
                secretPhase = _code
            };
            validation.Key = key;

            return validation.IsValid;
        }
    }
}
