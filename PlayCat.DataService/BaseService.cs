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
                    return ResponseBuilder<TReturn>
                       .Create()
                       .IsShowInfo(false)
                       .SetErrors(modelValidationResult.Errors)
                       .Fail()
                       .SetInfo("Model is not valid")
                       .Build();

                return func(request);
            }
            catch (Exception ex)
            {
                return ResponseBuilder<TReturn>.Create().Fail().SetInfoAndBuild(ex.Message);
            }
        }
    }
}
