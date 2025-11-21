using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel;

namespace Goodreads.Domain.Errors
{
    public static class GenreErrors
    {
        public static Error NotFound(string id) => Error.NotFound(
        "Genre.NotFound",
        $"The genere with id '{id}' was not found.");

        public static Error NotFound(List<string> ids) => Error.NotFound(
            "Genre.NotFound",
            $"The genres with ids '{string.Join(", ", ids)}' were not found.");
    }
}
