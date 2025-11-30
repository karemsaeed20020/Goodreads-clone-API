using Goodreads.Application.Common.Interfaces.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Quotes.Commands.DeleteQuote
{
    public record DeleteQuoteCommand(string QuoteId) : IRequest<Result>, IRequireQuoteAuthorization;
}
