using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class Result<T>
    {
        public bool Success { get; }
        public T? Data { get; }
        public string ErrorMessage { get; }
        public int StatusCode { get; }

        private Result(bool success, T? data, string errorMessage,int statusCode)
        {
            Success = success;
            Data = data;
            ErrorMessage = errorMessage;
            StatusCode = statusCode;
        }

        private Result(bool success, T? data, string errorMessage)
        {
            Success = success;
            Data = data;
            ErrorMessage = errorMessage;
        }

        public static Result<T> SuccessResult(T data)
        {
            return new Result<T>(true, data, string.Empty);
        }

        public static Result<T> FailureResult(string errorMessage,int statusCode)
        {
            return new Result<T>(false, default(T), errorMessage,statusCode);
        }
    }
}
