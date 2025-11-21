using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Authors.Queries.GetAuthorById
{
    public record GetAuthorByIdQuery(string Id) : IRequest<Result<AuthorDto>>;
}
