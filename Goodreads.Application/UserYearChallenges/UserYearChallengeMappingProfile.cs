using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.UserYearChallenges
{
    public class UserYearChallengeMappingProfile : Profile
    {
        public UserYearChallengeMappingProfile()
        {
            CreateMap<UserYearChallenge, UserYearChallengeDto>();
            CreateMap<UserYearChallenge, UserYearChallengeDetailsDto>();
        }
    }
}
