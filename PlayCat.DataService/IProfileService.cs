using PlayCat.DataService.Request;
using PlayCat.DataService.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayCat.DataService
{
    public interface IProfileService : ISetDbContext
    {
        GetUpdateProfileResult UpdateProfile(UpdateProfileRequest request);
        GetUpdateProfileResult GetProfile(Guid id);
    }
}
