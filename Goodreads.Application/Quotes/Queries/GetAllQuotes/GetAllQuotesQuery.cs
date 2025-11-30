using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Quotes.Queries.GetAllQuotes
{
    public record GetAllQuotesQuery(QueryParameters Parameters, string? Tag, string? UserId, string? AuthorId, string? BookId)
    : IRequest<PagedResult<QuoteDto>>;
}
