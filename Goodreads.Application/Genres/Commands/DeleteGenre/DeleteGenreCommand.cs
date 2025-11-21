using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Genres.Commands.DeleteGenre
{
    public record DeleteGenreCommand(string Id) : IRequest<Result>; 
}
