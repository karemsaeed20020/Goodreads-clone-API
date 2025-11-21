using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.AuthorClaims.Commands.ReviewAuthorClaim
{
    public record ReviewAuthorClaimCommand(string RequestId, bool Approve) : IRequest<Result>;
}
