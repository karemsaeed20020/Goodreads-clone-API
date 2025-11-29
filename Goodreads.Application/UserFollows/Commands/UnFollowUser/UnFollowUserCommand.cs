using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.UserFollows.Commands.UnFollowUser
{
    public record  UnFollowUserCommand(string FollowingId) : IRequest<Result>;
}
