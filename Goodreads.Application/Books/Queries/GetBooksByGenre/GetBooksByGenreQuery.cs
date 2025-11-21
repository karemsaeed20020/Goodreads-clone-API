using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Books.Queries.GetBooksByGenre
{
    public record GetBooksByGenreQuery(QueryParameters Parameters) : IRequest<PagedResult<BookDto>>;
    
}
