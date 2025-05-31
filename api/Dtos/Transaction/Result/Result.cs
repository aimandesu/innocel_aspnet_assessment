using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Transaction.Result
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public T? Data { get; set; } = default;
        public string ErrorMessage { get; set; } = string.Empty;

        public static Result<T> Success(T data) => new()
        {
            IsSuccess = true,
            Data = data
        };

        public static Result<T> Fail(string errorMessage) => new()
        {
            IsSuccess = false,
            ErrorMessage = errorMessage
        };
    }

}