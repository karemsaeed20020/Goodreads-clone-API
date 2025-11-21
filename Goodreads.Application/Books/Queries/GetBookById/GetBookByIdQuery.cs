using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Books.Queries.GetBookById
{
    public record GetBookByIdQuery(string Id) : IRequest<Result<BookDetailDto>>;
    
}
