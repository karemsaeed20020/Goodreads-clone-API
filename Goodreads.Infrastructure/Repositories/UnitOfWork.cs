using Goodreads.Application.Common.Interfaces;
using Goodreads.Domain.Entities;
using Goodreads.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Infrastructure.Repositories
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IRepository<Author>? _authorsRepository;
        private IRepository<Genre>? _genresRepository;
        private IRepository<Book>? _bookRepository;
        private IRepository<AuthorClaimRequest>? _authorClaimRequestRepository;
        private IRepository<ReadingProgress>? _readingProgressRepository;
        private IRepository<UserYearChallenge>? _userYearChallengeRepository;
        private IRepository<Quote>? _quoteRepository;
        private IRepository<QuoteLike>? _quoteLikeRepository;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
           
        }
        public IRepository<Author> Authors => _authorsRepository ??= new GenericRepository<Author>(_context);
        public IRepository<Book> Books => _bookRepository ??= new GenericRepository<Book>(_context);
        public IRepository<Genre> Genres => _genresRepository ??= new GenericRepository<Genre>(_context);
        public IRepository<AuthorClaimRequest> AuthorClaimRequests => _authorClaimRequestRepository ??=
                                            new GenericRepository<AuthorClaimRequest>(_context);

        //public IRepository<ReadingProgress> ReadingProgresses => _readingProgressRepository ??= new GenericRepository<ReadingProgress>(_context);

        public IRepository<ReadingProgress> ReadingPrgresses => _readingProgressRepository ??= new GenericRepository<ReadingProgress>(_context);
        public IRepository<UserYearChallenge> UserYearChallenges => _userYearChallengeRepository ??= new GenericRepository<UserYearChallenge>(_context);
        public IRepository<Quote> Quotes => _quoteRepository ??= new GenericRepository<Quote>(_context);
        public IRepository<QuoteLike> QuoteLikes => _quoteLikeRepository ??= new GenericRepository<QuoteLike>(_context);
        public IRepository<Shelf> Shelves => throw new NotImplementedException();

        public IRepository<BookShelf> BookShelves => throw new NotImplementedException();

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
        public void Dispose() => _context.Dispose();
    }
}
