using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.AuthorClaims.Commands.RequestAuthorClaim
{
    public record RequestAuthorClaimCommand(string AuthorId) : IRequest<Result>;
}
