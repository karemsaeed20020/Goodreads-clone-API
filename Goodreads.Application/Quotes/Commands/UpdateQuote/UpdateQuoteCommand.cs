using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Quotes.Commands.UpdateQuote
{
    public record UpdateQuoteCommand(string QuoteId, string Text, List<string>? Tags) : IRequest<Result>;
}
