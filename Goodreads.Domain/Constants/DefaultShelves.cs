using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Domain.Constants
{
    public static class DefaultShelves
    {
        public const string WantToRead = "Want to Read";
        public const string CurrentlyReading = "Currently Reading";
        public const string Read = "Read";

        public static readonly string[] All =
        {
        WantToRead,
        CurrentlyReading,
        Read
    };
    }
}
