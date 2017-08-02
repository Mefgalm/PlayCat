namespace PlayCat.DataService.Mappers
{
    public static class AuthTokenMapper
    {
        public static class ToApi
        {
            public static ApiModel.AuthToken FromData(DataModel.AuthToken dataToken)
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
