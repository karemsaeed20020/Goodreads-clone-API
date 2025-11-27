using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Domain.Entities
{
    public class ReadingProgress
    {
        public string UserId { get; set; } = default!;
        public string BookId { get; set; } = default!;
        public Book Book { get; set; }
        public int CurrentPage { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
