using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Infrastructure.Seeders
{
    public interface ISeeder
    {
        Task SeedAsync();
    }
}
