using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Books.Queries.GetBooksByGenre
{
    public class GetBooksByGenreQueryHandler : IRequestHandler<GetBooksByGenreQuery, PagedResult<BookDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetBooksByGenreQueryHandler> _logger;
        public GetBooksByGenreQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetBooksByGenreQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<PagedResult<BookDto>> Handle(GetBooksByGenreQuery request, CancellationToken cancellationToken)
        {
            var p = request.Parameters;
            _logger.LogInformation("Getting All Book for Genre {Genre}", p.Query);
            Expression<Func<Book, bool>> filter = b => b.BookGenres.Any(bg => bg.Genre.Name == p.Query);
            string[] includes = new[] { "Author", "BookGenres.Genre" };
            var (books, totalCount) = await _unitOfWork.Books.GetAllAsync(
            filter: filter,
            includes: includes,
            sortColumn: p.SortColumn,
            sortOrder: p.SortOrder,
            pageNumber: p.PageNumber,
            pageSize: p.PageSize
        );

            var bookDtos = _mapper.Map<List<BookDto>>(books);

            var pagedResult = PagedResult<BookDto>.Create(bookDtos, p.PageNumber, p.PageSize, totalCount);

            return pagedResult;
        }
    }
}
