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
    public class UserYearChallengeConfiguration : IEntityTypeConfiguration<UserYearChallenge>
    {
        public void Configure(EntityTypeBuilder<UserYearChallenge> builder)
        {
            builder.HasKey(uc => new { uc.UserId, uc.Year});
        }
    }
}
