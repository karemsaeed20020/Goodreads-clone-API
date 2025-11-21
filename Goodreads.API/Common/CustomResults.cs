using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Goodreads.API.Common
{
    public static class CustomResults
    {
        public static IActionResult Problem<T>(Result<T> result)
        {
            if (result.IsSuccess)
                throw new InvalidOperationException("Cannot return Problem() on successful result.");

            var problemDetails = new ProblemDetails
            {
                Title = result.Error.Code,
                Detail = result.Error.Description,
                //Status = GetStatusCode(result.Error.Type),
                //Type = GetLink(result.Error.Type)
            };

            return new ObjectResult(problemDetails)
            {
                StatusCode = problemDetails.Status
            };
        }


        public static IActionResult Problem(Result result)
        {
            if (result.IsSuccess)
                throw new InvalidOperationException("Cannot return Problem() on successful result.");

            var problemDetails = new ProblemDetails
            {
                Title = result.Error.Code,
                Detail = result.Error.Description,
                //Status = GetStatusCode(result.Error.Type),
                //Type = GetLink(result.Error.Type)
            };

            return new ObjectResult(problemDetails)
            {
                StatusCode = problemDetails.Status
            };
        }
    }
}