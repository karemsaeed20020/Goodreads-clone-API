using Goodreads.Application.Common.Interfaces.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Reviews.Commands.UpdateReview
{
    public record UpdateReviewCommand(string ReviewId, int? Rating, string? ReviewText)
    : IRequest<Result>, IRequireReviewAuthorization;

    public record UpdateReviewRequest(int? Rating, string? ReviewText);
}
