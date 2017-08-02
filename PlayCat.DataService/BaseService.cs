using Microsoft.Extensions.Logging;
using PlayCat.DataService.Response;
using PlayCat.Helpers;
using System;

namespace PlayCat.DataService
{
    public class BaseService
    {
        protected PlayCatDbContext _dbContext;
        protected ILogger _logger;

        public BaseService(PlayCatDbContext dbContext, ILogger logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public void SetDbContext(PlayCatDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private T GetUnexpectedServerError<T>(string message)
            where T : BaseResult, new()
        {
            return ResponseBuilder<T>
                    .Create()
                    .Fail()
                    .SetCode(ResponseCode.UnexpectedServerError)
                    .SetInfoAndBuild(message);
        }

        private void WriteLog(string message)
        {
            _logger.LogError(message);
        }

        protected TReturn RequestTemplate<TReturn>(Func<TReturn> func)
            where TReturn : BaseResult, new()
        {
            try
            {
                return func();
            } catch (Exception ex)
            {
                WriteLog(ex.Message);
                return GetUnexpectedServerError<TReturn>(ex.Message);
            }
        }

        protected TReturn RequestTemplateCheckModel<TReturn, TRequest>(TRequest request, Func<TReturn> func)
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

                return func();
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                return GetUnexpectedServerError<TReturn>(ex.Message);
            }
        }
    }
}
