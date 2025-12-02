using Goodreads.Application.Common.Interfaces.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Shelves.Commands.AddBookToShelf
{
    public record AddBookToShelfCommand(string ShelfId, string BookId) : IRequest<Result>, IRequireShelfAuthorization;
}
