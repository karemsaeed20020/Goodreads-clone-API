using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Shelves.Commands.CreateShelf
{
    public record CreateShelfCommand(string Name) : IRequest<Result<string>>;
}
