using Goodreads.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Genres.Queries.GetGenreById
{
    public class GetGenreByIdQueryHandler : IRequestHandler<GetGenreByIdQuery, Result<GenreDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private ILogger<GetGenreByIdQueryHandler> _logger; 
        public GetGenreByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetGenreByIdQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Result<GenreDto>> Handle(GetGenreByIdQuery request, CancellationToken cancellationToken)
        {
            var genre = await _unitOfWork.Genres.GetByIdAsync(request.Id);

            if (genre is null)
            {
                _logger.LogWarning("Genre with ID {GenreId} not found.", request.Id);
                return Result<GenreDto>.Fail(GenreErrors.NotFound(request.Id));
            }

            var dto = _mapper.Map<GenreDto>(genre);
            return Result<GenreDto>.Ok(dto); ;
        }
    }
}
