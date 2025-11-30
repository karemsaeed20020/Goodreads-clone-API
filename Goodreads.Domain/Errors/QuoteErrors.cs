using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Domain.Errors
{
    public static class QuoteErrors
    {
        public static Error NotFound(string id) => Error.NotFound(
            "Quote.NotFound",
            $"The quote with id '{id}' was not found"
            ); 
    }
}
