using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Shelves.Queries.GetUserShelves
{
    public record GetUserShelvesQuery(string UserId, QueryParameters Parameters, string? Shelf) : IRequest<PagedResult<ShelfDto>>;
}
