using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Authors.Queries.GetAllAuthors
{
    public class GetAllAuthorsQueryValidator : AbstractValidator<GetAllAuthorsQuery>
    {
        private readonly string[] allowedSortColumns = { "id", "name" };

        public GetAllAuthorsQueryValidator()
        {
            RuleFor(x => x.Parameters);
                

            RuleFor(x => x.Parameters.SortColumn)
                .Must(column => string.IsNullOrEmpty(column) || allowedSortColumns.Contains(column.ToLower()))
                .WithMessage($"Sort column must be one of the following: {string.Join(", ", allowedSortColumns)}");
        }
    }
}
