using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Auth.Commands.LoginUser
{
    public record LoginUserCommand(string UsernameOrEmail, string Password) : IRequest<Result<AuthResultDto>>;
}
