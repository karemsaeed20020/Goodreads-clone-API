using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Common.Interfaces.Authorization
{
    internal interface IRequireQuoteAuthorization
    {
        public string QuoteId { get; }

    }
}
