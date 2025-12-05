using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Reviews.Commands.CreateReview
{
    public record CreateReviewCommand(string BookId, int Rating, string ReviewText) : IRequest<Result<string>>;
}
