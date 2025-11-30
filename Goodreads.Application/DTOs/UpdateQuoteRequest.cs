using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.DTOs
{
    public record UpdateQuoteRequest(string Text, List<string>? Tags);
}
