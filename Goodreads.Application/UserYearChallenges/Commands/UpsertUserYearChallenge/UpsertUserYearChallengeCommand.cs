using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.UserYearChallenges.Commands.UpsertUserYearChallenge
{
    public record UpsertUserYearChallengeCommand(int TargetBooksCount) : IRequest<Result>;
}
