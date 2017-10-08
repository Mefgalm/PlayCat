using Microsoft.Extensions.Logging;
using PlayCat.DataService.Response;
using PlayCat.Helpers;
using System;
using System.Threading.Tasks;

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

        protected TReturn RequestTemplateWithTransaction<TReturn>(Func<TReturn> func)
            where TReturn : BaseResult, new()
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    TReturn result = func();

                    transaction.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    WriteLog(ex.Message);
                    return GetUnexpectedServerError<TReturn>(ex.Message);
                }
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
                       .Fail()
                       .IsShowInfo(false)
                       .SetErrors(modelValidationResult.Errors)
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

        protected async Task<TReturn> RequestTemplateCheckModelAsync<TReturn, TRequest>(TRequest request, Func<Task<TReturn>> func)
            where TReturn : BaseResult, new()
        {
            try
            {
                TrimStrings.Trim(request);

                ModelValidationResult modelValidationResult = ModelValidator.Validate(request);
                if (!modelValidationResult.Ok)
                    return ResponseBuilder<TReturn>
                       .Fail()
                       .IsShowInfo(false)
                       .SetErrors(modelValidationResult.Errors)
                       .SetInfo("Model is not valid")
                       .Build();

                return await func();
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                return GetUnexpectedServerError<TReturn>(ex.Message);
            }
        }
    }
}
