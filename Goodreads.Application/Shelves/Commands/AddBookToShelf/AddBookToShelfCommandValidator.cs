using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Shelves.Commands.AddBookToShelf
{
    public class AddBookToShelfCommandValidator : AbstractValidator<AddBookToShelfCommand>
    {
        public AddBookToShelfCommandValidator()
        {
            RuleFor(x => x.ShelfId).NotEmpty();
            RuleFor(x => x.BookId).NotEmpty();
        }
    }
}
