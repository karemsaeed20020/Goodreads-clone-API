using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.UserYearChallenges.Commands.UpsertUserYearChallenge
{
    public class UpsertUserYearChallengeCommandValidator : AbstractValidator<UpsertUserYearChallengeCommand>
    {
        public UpsertUserYearChallengeCommandValidator()
        {
            RuleFor(x => x.TargetBooksCount)
                .GreaterThan(0)
                .WithMessage("Target books count must be greater than zero.");
        }
    }
}
