using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Users
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserProfileDto>()
                .ForMember(dest => dest.Social, opt => opt.MapFrom(src => src.Social));

            CreateMap<Social, SocialDto>();

            CreateMap<User, UserDto>();
        }
    }
}
