using Goodreads.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Quotes.Commands.UpdateQuote
{
    public class UpdateQuoteCommandValidator : AbstractValidator<UpdateQuoteCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateQuoteCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleFor(x => x.QuoteId)
            .NotEmpty()
            .WithMessage("QuoteId is required")
            .MustAsync(QuoteExists)
             .WithMessage("Quote does not exist");


            RuleFor(x => x.Text)
               .NotEmpty().WithMessage("Text is required")
               .MaximumLength(500).WithMessage("Text cannot exceed 500 characters");

            RuleForEach(x => x.Tags ?? Enumerable.Empty<string>())
                .MaximumLength(50)
                .WithMessage("Each tag must not exceed 50 characters");

        }
        private async Task<bool> QuoteExists(string id, CancellationToken cancellationToken)
        {
            var quote = await _unitOfWork.Quotes.GetByIdAsync(id);
            return quote != null;
        }
    }
}
