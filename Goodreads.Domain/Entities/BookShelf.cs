using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Domain.Entities
{
    public class BookShelf
    {
        public string BookId { get; set; }
        public string ShelfId { get; set; }
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;

        public Book Book { get; set; } = null!;
        public Shelf Shelf { get; set; } = null!;
    }
}
