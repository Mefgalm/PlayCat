using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PlayCat.DataService.Request;
using PlayCat.DataService.Response;
using PlayCat.DataModel;
using PlayCat.DataService.Mappers;

namespace PlayCat.DataService
{
    public class ProfileService : BaseService, IProfileService
    {
        private const string UserNotFound = "User not found";

        public ProfileService(PlayCatDbContext dbContext, ILoggerFactory loggerFactory) 
            : base(dbContext, loggerFactory.CreateLogger<ProfileService>())
        {
        }

        public GetUpdateProfileResult GetProfile(Guid id)
        {
            return BaseInvoke(() =>
            {
                User dataUser = _dbContext.Users.FirstOrDefault(x => x.Id == id);

                if (dataUser == null)
                    return ResponseBuilder<GetUpdateProfileResult>.Fail().SetInfoAndBuild(UserNotFound);

                return ResponseBuilder<GetUpdateProfileResult>.SuccessBuild(new GetUpdateProfileResult()
                {
                    User = UserMapper.ToApi.FromData(dataUser),
                });
            });
        }

        public GetUpdateProfileResult UpdateProfile(UpdateProfileRequest request)
        {
            return BaseInvokeCheckModel(request, () =>
            {
                User dataUser = _dbContext.Users.FirstOrDefault(x => x.Id == request.Id);

                if (dataUser == null)
                    return ResponseBuilder<GetUpdateProfileResult>.Fail().SetInfoAndBuild(UserNotFound);

                dataUser.FirstName = request.FirstName;
                dataUser.LastName = request.LastName;
                dataUser.NickName = request.NickName;

                _dbContext.SaveChanges();

                return ResponseBuilder<GetUpdateProfileResult>.SuccessBuild(new GetUpdateProfileResult()
                {
                    User = UserMapper.ToApi.FromData(dataUser),
                });
            });
        }
    }
}
