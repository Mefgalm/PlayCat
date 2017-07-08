using PlayCat.DataService.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayCat.DataService.Mappers
{
    public static class UserMapper
    {
        public static class ToData
        {
            public static DataModel.User Get(ApiModel.User apiUser)
            {
                return apiUser is null ? null : new DataModel.User()
                {
                    Email = apiUser.Email,
                    FirstName = apiUser.FirstName,
                    LastName = apiUser.LastName,
                    IsUploadingAudio = apiUser.IsUploading,
                    Id = apiUser.Id,
                    NickName = apiUser.NickName,
                    RegisterDate = apiUser.RegisterDate,
                };
            }

            public static DataModel.User Get(SignUpRequest request, Action<DataModel.User> overrides = null)
            {
                var dataUser = request is null ? null : new DataModel.User()
                {
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    VerificationCode = request.VerificationCode,
                };
                overrides?.Invoke(dataUser);

                return dataUser;
            }
        }

        public static class ToApi
        {
            public static ApiModel.User Get(DataModel.User dataUser)
            {
                return dataUser is null ? null : new ApiModel.User()
                {
                    Email = dataUser.Email,
                    FirstName = dataUser.FirstName,
                    Id = dataUser.Id,
                    IsUploading = dataUser.IsUploadingAudio,
                    LastName = dataUser.LastName,
                    NickName = dataUser.NickName,
                    RegisterDate = dataUser.RegisterDate,
                };
            }
        }
    }
}
