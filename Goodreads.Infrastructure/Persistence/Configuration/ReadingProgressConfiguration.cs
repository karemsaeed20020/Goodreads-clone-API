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
    public class ReadingProgressConfiguration : IEntityTypeConfiguration<ReadingProgress>
    {
        public void Configure(EntityTypeBuilder<ReadingProgress> builder)
        {
            builder.HasKey(rp => new { rp.UserId, rp.BookId });
            builder.HasOne(rp => rp.Book)
                .WithMany()
                .HasForeignKey(rp => rp.BookId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
