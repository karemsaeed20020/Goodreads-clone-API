using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Domain.Entities
{
    public class Quote : BaseEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Text { get; set; } = default!;
        public string? AuthorId { get; set; }
        public string? BookId { get; set; }
        public string CreatedByUserId { get; set; } = default!;
        public List<string> Tags { get; set; } = new List<string>();

        public ICollection<QuoteLike> Likes { get; set; } = new List<QuoteLike>();
        public int LikesCount => Likes?.Count ?? 0;
    }
}
