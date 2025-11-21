using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Domain.Errors
{
    public class BookErrors
    {
        public static Error NotFound(string id) => Error.NotFound(
    "Book.NotFound",
    $"The book with id '{id}' was not found.");
    }
}
