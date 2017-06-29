using PlayCat.DataService.Response;

namespace PlayCat.DataService
{
    public interface IResponse<T>
        where T : BaseResult
    {
        T Source { get; }
        T Fail(string info);
        T Success(string info = null);
    }
}
