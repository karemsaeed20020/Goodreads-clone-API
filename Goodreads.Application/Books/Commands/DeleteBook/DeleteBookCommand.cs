using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Books.Commands.DeleteBook
{
    public record DeleteBookCommand(string Id) : IRequest<Result>;
}
