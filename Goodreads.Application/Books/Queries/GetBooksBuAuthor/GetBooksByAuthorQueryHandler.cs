using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;



namespace Goodreads.Application.Books.Queries.GetBooksBuAuthor
{
    public class GetBooksByAuthorQueryHandler : IRequestHandler<GetBooksBuAuthorQuery, Common.Responses.PagedResult<BookDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetBooksByAuthorQueryHandler> _logger;
        public GetBooksByAuthorQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetBooksByAuthorQueryHandler> looger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = looger;
        }
        public async Task<Common.Responses.PagedResult<BookDto>> Handle(GetBooksBuAuthorQuery request, CancellationToken cancellationToken)
        {
            var p = request.Parameters;
            _logger.LogInformation("Getting All Book for Author {AuthorId}", request.AuthorId);
            Expression<Func<Book, bool>> filter = b => b.AuthorId == request.AuthorId;
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
            var pagedResult = Common.Responses.PagedResult<BookDto>.Create(bookDtos, p.PageNumber, p.PageSize, totalCount);

            return pagedResult;
        }
    }
}
