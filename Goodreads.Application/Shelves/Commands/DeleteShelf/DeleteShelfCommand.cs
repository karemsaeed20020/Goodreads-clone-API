using Goodreads.Application.Common.Interfaces.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Shelves.Commands.DeleteShelf
{
    public record DeleteShelfCommand(string ShelfId) : IRequest<Result>, IRequireShelfAuthorization;
}
