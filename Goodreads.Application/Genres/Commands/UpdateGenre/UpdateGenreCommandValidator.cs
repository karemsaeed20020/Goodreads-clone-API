using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Genres.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(x => x.Id)
           .NotEmpty()
           .WithMessage("Genre ID is required.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Genre name is required.")
                .MaximumLength(100)
                .WithMessage("Genre name must not exceed 100 characters.");
        }
    }
}
