using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Domain.Entities
{
    public class BookGenre
    {
        public string BookId { get; set; }
        public Book Book { get; set; }

        public string GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
