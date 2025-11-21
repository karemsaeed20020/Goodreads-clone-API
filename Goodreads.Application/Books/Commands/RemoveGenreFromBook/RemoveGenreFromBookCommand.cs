using Goodreads.Application.Common.Interfaces.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Books.Commands.RemoveGenreFromBook
{
    public record  RemoveGenreFromBookCommand(string BookId, string GenreId) : IRequest<Result>, IRequireBookAuthorization;
}

