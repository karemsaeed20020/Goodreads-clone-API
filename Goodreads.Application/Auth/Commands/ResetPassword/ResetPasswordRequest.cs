using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Goodreads.Application.Auth.Commands.ResetPassword
{
    public record ResetPasswordRequest(
    [property: JsonPropertyName("newPassword")] string NewPassword
);
}
