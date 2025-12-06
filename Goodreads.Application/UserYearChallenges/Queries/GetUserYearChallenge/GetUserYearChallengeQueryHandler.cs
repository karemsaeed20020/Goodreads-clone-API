using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.UserYearChallenges.Queries.GetUserYearChallenge
{
    public class GetUserYearChallengeQueryHandler : IRequestHandler<GetUserYearChallengeQuery, Result<UserYearChallengeDetailsDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserYearChallengeQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<UserYearChallengeDetailsDto>> Handle(GetUserYearChallengeQuery request, CancellationToken cancellationToken)
        {
            var userId = request.UserId;
            var year = request.Year;

            var challenge = await _unitOfWork.UserYearChallenges.GetSingleOrDefaultAsync(
                filter: c => c.UserId == userId && c.Year == year);

            if (challenge == null)
                return Result<UserYearChallengeDetailsDto>.Fail(UserYearChallengeErrors.NotFound(year));

           // Book Shelves DO TASK


            var challengeDto = _mapper.Map<UserYearChallengeDetailsDto>(challenge);

            return Result<UserYearChallengeDetailsDto>.Ok(challengeDto);
        }
    }
}
