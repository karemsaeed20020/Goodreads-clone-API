using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Domain.Entities
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; } = default;
        public string? LastName { get; set; } = default;
        public DateOnly? DateOfBirth { get; set; }
        public string? ProfilePictureUrl { get; set; } = default;
        public string? ProfilePictureBlobName { get; set; } = default;
        public string? Bio { get; set; } = default;
        public string? WebsiteUrl { get; set; }
        public string? Country { get; set; } = default!;
        public Social Social { get; set; } = default!;
        public Author? ClaimedAuthorProfile { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public class Social
    {
        public string? Facebook { get; set; }
        public string? Twitter { get; set; }
        public string? Linkedin { get; set; }
    }
}
