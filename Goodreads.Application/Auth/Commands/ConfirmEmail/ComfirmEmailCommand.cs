using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Auth.Commands.Confirm_Email
{
    public record ConfirmEmailCommand(string UserId, string Token) : IRequest<Result<bool>>;
}
