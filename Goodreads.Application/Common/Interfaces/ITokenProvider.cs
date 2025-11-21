using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Common.Interfaces
{
    public interface ITokenProvider
    {
        Task<string> GenerateAccessTokenAsync(User user);
        Task<string> GenerateAndStoreRefreshTokenAsync(User user, string jwtId);
    }
}
