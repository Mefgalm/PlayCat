using PlayCat.DataService.Response;
using PlayCat.Helpers;
using System;

namespace PlayCat.DataService
{
    public class BaseService
    {
        protected PlayCatDbContext _dbContext;

        public BaseService(PlayCatDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SetDbContext(PlayCatDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected TReturn RequestTemplate<TReturn, TRequest>(TRequest request, Func<TRequest, TReturn> func)
            where TReturn : BaseResult, new()
        {
            try
            {
                TrimStrings.Trim(request);

                ModelValidationResult modelValidationResult = ModelValidator.Validate(request);
                if (!modelValidationResult.Ok)
                    return ResponseFactory.With(new TReturn()
                    {
                        Errors = modelValidationResult.Errors
                    })
                    .HideInfo()
                    .Fail("Model is not valid");

                return func(request);
            }
            catch (Exception ex)
            {
                return ResponseFactory.With<TReturn>().Fail(ex.Message);
            }
        }
    }
}
