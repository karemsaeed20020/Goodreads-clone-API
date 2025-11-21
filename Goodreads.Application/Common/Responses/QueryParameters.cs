using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Common.Responses
{
    public record QueryParameters(
        string? Query,
    string? SortColumn,
    string? SortOrder,
    int? PageNumber,
    int? PageSize
        );
    
}
