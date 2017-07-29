using System;
using System.Collections.Generic;
using PlayCat.DataService.Response;

namespace PlayCat.DataService
{
    public class ResponeImp<T> : IResponse<T>
        where T : BaseResult
    {
        public ResponeImp(T obj)
        {
            Source = obj;
        }

        public IResponse<T> ShowInfo()
        {
            Source.ShowInfo = true;
            return this;
        }

        public IResponse<T> HideInfo()
        {
            Source.ShowInfo = false;
            return this;
        }

        public T Source { get; private set; }

        public T Fail(string info, ResponseCode code = ResponseCode.None)
        {
            Source.Ok = false;
            Source.Info = info;
            Source.Code = code;
            return Source;
        }

        public T Fail(string info, IDictionary<string, string> errors, ResponseCode code = ResponseCode.None)
        {
            Source.Ok = false;
            Source.Info = info;
            Source.Errors = errors;
            Source.Code = code;
            return Source;
        }

        public T Success(string info = null)
        {
            Source.Ok = true;
            Source.Info = info;
            return Source;
        }
    }
}
