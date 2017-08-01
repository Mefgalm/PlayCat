namespace PlayCat.DataService.Mappers
{
    public static class AuthTokenMapper
    {
        public static class ToApi
        {
            public static ApiModel.AuthToken Get(DataModel.AuthToken dataToken)
            {
                return dataToken == null ? null : new ApiModel.AuthToken()
                {
                    Id = dataToken.Id,
                    DateExpired = dataToken.DateExpired,                       
                };
            }
        }
    }
}
