using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Quotes.Queries.GetQuoteById
{
    public record GetQuoteByIdQuery(string Id) : IRequest<Result<QuoteDto>>;
}
