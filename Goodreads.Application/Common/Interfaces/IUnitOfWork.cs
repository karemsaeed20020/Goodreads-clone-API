using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Common.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Author> Authors { get; }
        IRepository<Book> Books { get; }
        IRepository<Genre> Genres { get; }
        IRepository<Shelf> Shelves { get; }
        IRepository<BookShelf> BookShelves { get; }
        IRepository<AuthorClaimRequest> AuthorClaimRequests { get; }
        IRepository<ReadingProgress> ReadingPrgresses { get; }
        IRepository<UserYearChallenge> UserYearChallenges { get; }
        IRepository<Quote> Quotes { get; }
        IRepository<QuoteLike> QuoteLikes { get; }
        Task<int> SaveChangesAsync();
    }
}
