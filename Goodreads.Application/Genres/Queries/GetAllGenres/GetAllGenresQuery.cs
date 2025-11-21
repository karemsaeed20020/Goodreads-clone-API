using Goodreads.Application.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Genres.Queries.GetAllGenres
{
    public record GetAllGenresQuery(QueryParameters Parameters) : IRequest<Result<List<GenreDto>>>;
}
