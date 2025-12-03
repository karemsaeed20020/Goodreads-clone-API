using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Users.Queries.GetAllUsers
{
    public record GetAllUsersQuery(QueryParameters Parameters) : IRequest<PagedResult<UserDto>>;
}
