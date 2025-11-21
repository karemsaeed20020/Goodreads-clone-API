using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Auth.Commands.ResetEmailConfirmation
{
    public record ResetEmailConfirmationCommand(string email) : IRequest<Result<string>>;
}
