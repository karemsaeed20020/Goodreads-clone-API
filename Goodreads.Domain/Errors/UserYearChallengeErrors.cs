using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Domain.Errors
{
    public static class UserYearChallengeErrors
    {
        public static Error NotFound(int year) => Error.NotFound(
                "UserYearChallenge.NotFound",
                $"Year challenge not found for year {year}"
            );
    }
}
