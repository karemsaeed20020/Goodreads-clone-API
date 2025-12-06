using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Users.Commands.UpdateUserProfile
{
    public class UpdateUserProfileCommand : IRequest<Result>
    {
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public DateOnly? DateOfBirth { get; init; }
        public string? Bio { get; init; }
        public string? WebsiteUrl { get; init; }
        public string? Country { get; init; }
    }
}
