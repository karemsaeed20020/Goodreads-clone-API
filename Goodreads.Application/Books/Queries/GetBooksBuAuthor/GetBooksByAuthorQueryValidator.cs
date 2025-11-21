using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Books.Queries.GetBooksBuAuthor
{
    public class GetBooksByAuthorQueryValidator : AbstractValidator<GetBooksBuAuthorQuery>
    {
        private readonly string[] allowedSortColumns = { "title", "language", "publisher" };
        public GetBooksByAuthorQueryValidator()
        {
            RuleFor(x => x.AuthorId).NotEmpty();
            RuleFor(x => x.Parameters.Query)
                .NotEmpty().WithMessage("Query cannot be empty.");
            RuleFor(x => x.Parameters.SortColumn)
                .Must(sortColumn => string.IsNullOrEmpty(sortColumn) || allowedSortColumns.Contains(sortColumn.ToLower()))
                .WithMessage($"SortColumn must be one of the following: {string.Join(", ", allowedSortColumns)}");
        }
    }
}
