using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Domain.Entities
{
    public class UserYearChallenge
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public int Year { get; set; }
        public int TargetBooksCount { get; set; } = 0;
        public int CompletedBooksCount { get; set; } = 0;
    }
}
