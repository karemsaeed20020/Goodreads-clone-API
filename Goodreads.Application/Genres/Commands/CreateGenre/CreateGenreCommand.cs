using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Genres.Commands.CreateGenre
{
    public record CreateGenreCommand(string Name) : IRequest<Result<string>>;
}
