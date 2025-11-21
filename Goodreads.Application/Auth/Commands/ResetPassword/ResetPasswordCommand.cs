using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Auth.Commands.ResetPassword
{
    public record ResetPasswordCommand(string userId, string Token, string NewPassword) : IRequest<Result>;
}
