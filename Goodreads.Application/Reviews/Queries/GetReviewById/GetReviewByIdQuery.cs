using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Reviews.Queries.GetReviewById
{
    public record GetReviewByIdQuery(string ReviewId) : IRequest<Result<BookReviewDto>>;
}
