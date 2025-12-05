using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Reviews.Queries.GetAllReviews
{
    public record GetAllReviewsQuery(QueryParameters Parameters, string? UserId, string? Bookid) : IRequest<PagedResult<BookReviewDto>>;
}
