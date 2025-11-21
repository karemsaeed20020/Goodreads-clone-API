using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Domain.Errors
{
    public static class AuthorErrors
    {
        public static Error NotFound(string id) => Error.NotFound(
         "Author.NotFound",
         $"The author with id '{id}' was not found.");



    }
}
