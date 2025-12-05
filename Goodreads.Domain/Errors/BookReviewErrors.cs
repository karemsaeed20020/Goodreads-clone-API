using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Domain.Errors
{
    public static class BookReviewErrors
    {
        public static Error NotFound(string reviewId) => Error.NotFound("Reviews.NotFound", $"Review with ID '{reviewId}' not found.");
        public static Error AlreadyReviewed(string bookId) => Error.Conflict("Reviews.AlreadyReviewed", $"You have already reviewed the book with ID '{bookId}'.");
    }
}
