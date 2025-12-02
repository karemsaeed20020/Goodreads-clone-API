using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Shelves.Queries.GetShelfById
{
    public record GetShelfByIdQuery(string Id) : IRequest<Result<ShelfDto>>;
}
