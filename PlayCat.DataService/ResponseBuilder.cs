using PlayCat.DataService.Response;
using System;
using System.Collections.Generic;

namespace PlayCat.DataService
{
    public class ResponseBuilder<T>
        where T : BaseResult, new()
    {
        private T _response;

        protected ResponseBuilder(T response) 
        {
            _response = response;
        }

        public static ResponseBuilder<T> Create()
        {            
            return new ResponseBuilder<T>(new T());
        }

        public ResponseBuilder<T> SetInfo(string info)
        {
            _response.Info = info;
            return this;
        }

        public T SetInfoAndBuild(string info)
        {
            _response.Info = info;
            return _response;
        }

        public static ResponseBuilder<T> Fail()
        {
            var obj = new T()
            {
                Ok = false,
            };
            return new ResponseBuilder<T>(obj);
        }

        public ResponseBuilder<T> IsShowInfo(bool isShow)
        {
            _response.ShowInfo = isShow;
            return this;
        }

        public ResponseBuilder<T> Success()
        {
            var obj = new T()
            {
                Ok = true,
            };
            return new ResponseBuilder<T>(obj);
        }

        public ResponseBuilder<T> SetErrors(IDictionary<string, string> errorDictinary)
        {
            _response.Errors = errorDictinary;
            return this;
        }

        public ResponseBuilder<T> SetCode(ResponseCode responseCode)
        {
            _response.Code = responseCode;
            return this;
        }

        public T Build()
        {
            return _response;
        }

        public static T SuccessBuild()
        {
            return new T()
            {
                Ok = true,
            };
        }

        public static T SuccessBuild(T response)
        {
            if (!response.Ok)
                throw new Exception("Wrong result");

            return response;
        }
    }
}
