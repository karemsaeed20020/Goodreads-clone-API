using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.AuthorClaims
{
    public class AuthorClaimRequestMappingProfile : Profile
    {
        public AuthorClaimRequestMappingProfile()
        {
            CreateMap<AuthorClaimRequest, AuthorClaimRequestDto>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name))
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User.Email));
        }
    }
}
