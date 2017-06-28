namespace PlayCat.DataService.ReturnTypes
{
    public class BaseResult<T>
        where T : BaseResult<T>, new()
    {
        private static readonly T DefaultSuccess;

        public bool Ok { get; set; }
        public string Error { get; set; }

        static BaseResult()
        {
            DefaultSuccess = new T()
            {
                Ok = true
            };
        }

        public T Success() 
        {
            return DefaultSuccess;
        }

        public T Fail(string error)
        {
            return new T()
            {
                Error = error,
            };
        }
    }
}
