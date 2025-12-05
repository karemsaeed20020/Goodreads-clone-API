using Goodreads.Application.Common.Interfaces.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Reviews.Commands.DeleteReview
{
    public record DeleteReviewCommand(string ReviewId) : IRequest<Result>, IRequireReviewAuthorization;
}
