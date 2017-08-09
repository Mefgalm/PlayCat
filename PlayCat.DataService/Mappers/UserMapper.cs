using PlayCat.DataService.Request;
using System;

namespace PlayCat.DataService.Mappers
{
    public static class UserMapper
    {
        public static class ToData
        {
            public static DataModel.User FromRequest(SignUpRequest request, Action<DataModel.User> overrides = null)
            {
                var dataUser = request == null ? null : new DataModel.User()
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
            public static ApiModel.User FromData(DataModel.User dataUser)
            {
                return dataUser == null ? null : new ApiModel.User()
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
