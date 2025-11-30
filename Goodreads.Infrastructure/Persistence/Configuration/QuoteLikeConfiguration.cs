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
    public class QuoteLikeConfiguration : IEntityTypeConfiguration<QuoteLike>
    {
        public void Configure(EntityTypeBuilder<QuoteLike> builder)
        {
            builder.HasKey(q => new { q.QuoteId, q.UserId });

            builder.HasOne(q => q.Quote)
                   .WithMany(q => q.Likes)
                   .HasForeignKey(q => q.QuoteId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(q => q.User)
                   .WithMany(u => u.LikedQuotes)
                   .HasForeignKey(q => q.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
