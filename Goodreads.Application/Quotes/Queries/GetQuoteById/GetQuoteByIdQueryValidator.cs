using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Quotes.Queries.GetQuoteById
{
    public class GetQuoteByIdQueryValidator : AbstractValidator<GetQuoteByIdQuery>
    {
        public GetQuoteByIdQueryValidator()
        {
            RuleFor(q => q.Id)
                .NotEmpty().WithMessage("Quote ID must not be empty.")
                .Must(id => string.IsNullOrEmpty(id) || Guid.TryParse(id, out _))
                .WithMessage("QuoteId must be a valid GUID");
        }
    }
}
