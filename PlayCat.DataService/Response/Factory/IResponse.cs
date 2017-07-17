using PlayCat.DataService.Response;

namespace PlayCat.DataService
{
    public interface IResponse<T>
        where T : BaseResult
    {
        T Source { get; }
        T Fail(string info);
        IResponse<T> ShowInfo();
        IResponse<T> HideInfo();
        T Success(string info = null);
    }
}
