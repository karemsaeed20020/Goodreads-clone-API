using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.ReadingProgresses.Queries.GetReadingProgresses
{
    public record GetReadingProgressesQuery(QueryParameters Parameters) : IRequest<PagedResult<ReadingProgressDto>>;
}
