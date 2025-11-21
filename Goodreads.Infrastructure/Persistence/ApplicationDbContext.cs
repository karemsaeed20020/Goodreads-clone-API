using Goodreads.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        // Add this DbSet for RefreshToken
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<AuthorClaimRequest> AuthorClaimRequests { get; set; }
        public DbSet<Genre> Genres { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Social as an owned type
            modelBuilder.Entity<User>(entity =>
            {
                entity.OwnsOne(u => u.Social, social =>
                {
                    social.Property(s => s.Facebook).HasMaxLength(500);
                    social.Property(s => s.Twitter).HasMaxLength(500);
                    social.Property(s => s.Linkedin).HasMaxLength(500);
                });
            });

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}