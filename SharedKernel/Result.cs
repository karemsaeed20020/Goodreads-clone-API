using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel
{
    public class Result
    {
        public bool IsSuccess { get; protected init; }
        public Error Error { get; protected init; } = Error.None;

        public static Result Ok() => new() { IsSuccess = true };
        public static Result Fail(Error error) => new() { IsSuccess = false, Error = error };

       
    }

    public class Result<T> : Result
    {
        public T? Data { get; private init; }

        public static Result<T> Ok(T value) => new() { IsSuccess = true, Data = value };
        public static new Result<T> Fail(Error error) => new() { IsSuccess = false, Error = error };
        
    }
}
