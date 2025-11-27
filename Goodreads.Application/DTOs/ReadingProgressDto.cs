using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.DTOs
{
    public class ReadingProgressDto
    {
        public string Title { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int CurrentPage { get; set; }
        public double ProgressPercent { get; set; }
        public string BookId { get; set; }
    }
}
