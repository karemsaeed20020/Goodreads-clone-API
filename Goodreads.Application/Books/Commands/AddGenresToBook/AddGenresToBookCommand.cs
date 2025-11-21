using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Books.Commands.AddGenresToBook
{
    public record AddGenresToBookCommand(string BookId, List<string> GenreIds) : IRequest<Result>;

}
