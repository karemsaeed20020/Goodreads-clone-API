using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel
{
    public static class ResultExensions
    {
        public static TResult Match<T, TResult>(
        this Result<T> result,
        Func<T, TResult> onSuccess,
        Func<Result<T>, TResult> onFailure)
        {
            return result.IsSuccess ? onSuccess(result.Data!) : onFailure(result);
        }

        public static TResult Match<TResult>(
          this Result result,
          Func<TResult> onSuccess,
          Func<Result, TResult> onFailure)
        {
            return result.IsSuccess ? onSuccess() : onFailure(result);
        }
    }
}
