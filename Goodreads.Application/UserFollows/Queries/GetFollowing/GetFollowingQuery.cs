using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.UserFollows.Queries.GetFollowing
{
    public record GetFollowingQuery(int? PageNumber, int? PageSize) : IRequest<Result<PagedResult<UserDto>>>;
}
