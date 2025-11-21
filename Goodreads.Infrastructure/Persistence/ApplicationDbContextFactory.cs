using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Goodreads.Infrastructure.Persistence
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            //"DefaultConnection": "Server=.;Database=RecyclingSystemDB;Trusted_Connection=true;TrustServerCertificate=true;"

            var connectionString = "Server=.;Database=GoodReadsDB;Trusted_Connection=true;TrustServerCertificate=true;";

            optionsBuilder.UseSqlServer(connectionString);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}