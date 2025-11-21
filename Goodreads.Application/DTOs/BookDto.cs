using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.DTOs
{
    public class BookDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string CoverImageUrl { get; set; }
        public string AuthorName { get; set; }
        public string Publisher { get; set; }
        public string Language { get; set; }
        public List<BookGenreDto> Genres { get; set; }
        public double AverageRating { get; set; } = 0.0;
    }
}
