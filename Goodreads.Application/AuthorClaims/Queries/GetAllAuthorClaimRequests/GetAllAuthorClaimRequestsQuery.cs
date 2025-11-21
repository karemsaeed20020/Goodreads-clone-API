using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.AuthorClaims.Queries.GetAllAuthorClaimRequests
{
    public record GetAllAuthorClaimRequestsQuery(string? SortColumn, string? SortOrder, int? PageNumber, int? PageSize, ClaimRequestStatus? Status = null)
        : IRequest<PagedResult<AuthorClaimRequestDto>>;
}
