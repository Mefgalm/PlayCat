using PlayCat.DataService.Response;

namespace PlayCat.DataService
{
    public class ResponseFactory
    {
        public static IResponse<T> With<T>(T obj)
            where T : BaseResult
        {
            return new ResponeImp<T>(obj);
        }

        public static IResponse<T> With<T>()
            where T : BaseResult, new()
        {
            return new ResponeImp<T>(new T());
        }
    }
}
