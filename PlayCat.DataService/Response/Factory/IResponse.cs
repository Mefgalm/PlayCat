using PlayCat.DataService.Response;
using System.Collections.Generic;

namespace PlayCat.DataService
{
    public interface IResponse<T>
        where T : BaseResult
    {
        T Source { get; }
        T Fail(string info);
        T Fail(string info, IDictionary<string, string> errors);
        IResponse<T> ShowInfo();
        IResponse<T> HideInfo();
        T Success(string info = null);
    }
}
