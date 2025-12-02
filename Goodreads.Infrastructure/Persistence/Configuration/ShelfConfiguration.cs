using Goodreads.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Infrastructure.Persistence.Configuration
{
    public class ShelfConfiguration : IEntityTypeConfiguration<Shelf>
    {
        public void Configure(EntityTypeBuilder<Shelf> builder)
        {
            builder.Property(s => s.Name)
                .HasMaxLength(100);

            builder.HasMany(s => s.BookShelves)
                .WithOne(bs => bs.Shelf)
                .HasForeignKey(bs => bs.ShelfId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
