using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Shelves.Commands.CreateShelf
{
    public class CreateShelfCommandValidator : AbstractValidator<CreateShelfCommand>
    {
        public CreateShelfCommandValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Shelf name is required.")
            .MaximumLength(100).WithMessage("Shelf name must not exceed 100 characters.");
        }
    }
}
