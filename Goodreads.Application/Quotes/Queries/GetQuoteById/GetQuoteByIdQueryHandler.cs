using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Quotes.Queries.GetQuoteById
{
    public class GetQuoteByIdQueryHandler : IRequestHandler<GetQuoteByIdQuery,Result<QuoteDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetQuoteByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<QuoteDto>> Handle(GetQuoteByIdQuery request, CancellationToken cancellationToken)
        {
            var quote = await _unitOfWork.Quotes.GetByIdAsync(request.Id);
            if (quote == null)
                return Result<QuoteDto>.Fail(QuoteErrors.NotFound(request.Id));

            var quoteDto = _mapper.Map<QuoteDto>(quote);
            return Result<QuoteDto>.Ok(quoteDto);
        }
    }
}
