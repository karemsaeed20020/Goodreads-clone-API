using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.UserYearChallenges.Queries.GetAllUserYearChallenges
{
    public record GetAllUserYearChallengesQuery(string UserId, QueryParameters Parameters, int? Year) :IRequest<PagedResult<UserYearChallengeDto>>;
}
