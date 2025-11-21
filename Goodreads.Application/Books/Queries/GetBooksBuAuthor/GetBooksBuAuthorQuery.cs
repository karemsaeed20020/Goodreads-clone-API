using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Books.Queries.GetBooksBuAuthor
{
    public record GetBooksBuAuthorQuery(string AuthorId, QueryParameters Parameters) : IRequest<Common.Responses.PagedResult<BookDto>>;    
}
