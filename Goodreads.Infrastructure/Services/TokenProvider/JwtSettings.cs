using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Infrastructure.Services.TokenProvider
{
    public class JwtSettings
    {
        public const string Section = "JwtSettings";
        public string Secret { get; set; } = default!;
        public int TokenExpirationInMinutes { get; set; }
        public string Issuer { get; set; } = default!;
        public string Audience { get; set; } = default!;
    }
}
