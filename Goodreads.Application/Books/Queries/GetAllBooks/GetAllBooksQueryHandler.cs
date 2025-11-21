using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Books.Queries.GetAllBooks
{
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, PagedResult<BookDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllBooksQueryHandler> _logger;
        public GetAllBooksQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetAllBooksQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<PagedResult<BookDto>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetAllBooksQuery with parameters: {@Parameters}", request.Parameters);
            var p = request.Parameters;

            Expression<Func<Book, bool>> filter = book =>
                string.IsNullOrEmpty(p.Query)
                || book.Title.Contains(p.Query)
                || book.Author.Name.Contains(p.Query)
                || book.BookGenres.Any(bg => bg.Genre.Name.Contains(p.Query));

            var (books, totalCount) = await _unitOfWork.Books.GetAllAsync(
                filter: filter,
                includes: new[] { "Author", "BookGenres.Genre" },
                sortColumn: p.SortColumn,
                sortOrder: p.SortOrder,
                pageNumber: p.PageNumber,
                pageSize: p.PageSize
            );

            var bookDtos = _mapper.Map<List<BookDto>>(books);

            return PagedResult<BookDto>.Create(bookDtos, p.PageNumber, p.PageSize, totalCount);
        }
    }
}
