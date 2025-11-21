using Goodreads.Application.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Authors.Queries.GetAllAuthors
{
    public record GetAllAuthorsQuery(QueryParameters Parameters) : IRequest<Common.Responses.PagedResult<AuthorDto>>;
}
