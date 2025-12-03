using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Users.Queries.GetProfileByUsername
{
    public record GetProfileByUsernameQuery(string Username) : IRequest<Result<UserProfileDto>>;
}
