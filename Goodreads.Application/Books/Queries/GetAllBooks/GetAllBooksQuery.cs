using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Books.Queries.GetAllBooks
{
    public record  GetAllBooksQuery(QueryParameters Parameters) : IRequest<PagedResult<BookDto>>;
    
}
