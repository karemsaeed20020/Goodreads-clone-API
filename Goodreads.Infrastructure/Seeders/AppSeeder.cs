using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Infrastructure.Seeders
{
    public class AppSeeder(IServiceProvider serviceProvider)
    {
        public async Task SeedAsync()
        {
            using var scope = serviceProvider.CreateScope();
            var seeders = scope.ServiceProvider.GetServices<ISeeder>();

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync();
            }
        }
    }
}
