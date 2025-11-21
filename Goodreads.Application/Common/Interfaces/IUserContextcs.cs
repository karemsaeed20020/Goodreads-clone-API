using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Common.Interfaces
{
    public interface IUserContext
    {
        string? UserId { get; }
        bool IsInRole(string role);
    }
}
