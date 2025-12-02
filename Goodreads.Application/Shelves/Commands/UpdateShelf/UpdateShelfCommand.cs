using Goodreads.Application.Common.Interfaces.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Shelves.Commands.UpdateShelf
{
    public record UpdateShelfCommand(string ShelfId, string Name) : IRequest<Result>, IRequireShelfAuthorization;
}
