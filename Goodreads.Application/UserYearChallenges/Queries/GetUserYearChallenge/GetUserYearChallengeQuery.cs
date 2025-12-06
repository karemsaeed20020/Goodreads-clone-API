using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.UserYearChallenges.Queries.GetUserYearChallenge
{
    public record GetUserYearChallengeQuery(string UserId, int Year) : IRequest<Result<UserYearChallengeDetailsDto>>;
}
