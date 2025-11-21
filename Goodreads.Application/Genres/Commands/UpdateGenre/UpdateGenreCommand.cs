using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Genres.Commands.UpdateGenre
{
    public record UpdateGenreCommand(string Id, string Name) : IRequest<Result>;
}
