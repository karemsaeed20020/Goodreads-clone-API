using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Books.Commands.RemoveGenreFromBook
{
    public class RemoveGenreFromBookCommandValidator : AbstractValidator<RemoveGenreFromBookCommand>
    {
        public RemoveGenreFromBookCommandValidator()
        {
            RuleFor(x => x.BookId).NotEmpty();

            RuleFor(x => x.GenreId).NotEmpty();
        }

    }
}
