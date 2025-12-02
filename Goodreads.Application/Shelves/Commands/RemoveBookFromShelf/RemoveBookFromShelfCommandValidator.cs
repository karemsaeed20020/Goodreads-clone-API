using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Shelves.Commands.RemoveBookFromShelf
{
    public class RemoveBookFromShelfCommandValidator : AbstractValidator<RemoveBookFromShelfCommand>
    {
        public RemoveBookFromShelfCommandValidator()
        {
            RuleFor(x => x.ShelfId).NotEmpty();
            RuleFor(x => x.BookId).NotEmpty();
        }
    }
}
