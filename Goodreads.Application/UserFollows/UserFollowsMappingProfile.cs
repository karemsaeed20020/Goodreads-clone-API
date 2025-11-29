using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.UserFollows
{
    public class UserFollowsMappingProfile : Profile
    {
        public UserFollowsMappingProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
