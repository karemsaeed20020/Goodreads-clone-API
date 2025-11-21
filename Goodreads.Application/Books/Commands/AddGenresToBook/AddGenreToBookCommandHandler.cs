using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Books.Commands.AddGenresToBook
{
    public class AddGenreToBookCommandHandler : IRequestHandler<AddGenresToBookCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AddGenreToBookCommandHandler> _logger;
        public AddGenreToBookCommandHandler(IUnitOfWork unitOfWork, ILogger<AddGenreToBookCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<Result> Handle(AddGenresToBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(request.BookId);
            if (book == null)
            {
                _logger.LogWarning("Book with ID {BookId} not found.", request.BookId);
                return Result.Fail(BookErrors.NotFound(request.BookId));
            }
            var existingGenreIds = book.BookGenres.Select(bg => bg.GenreId);
            var newGenreIds = request.GenreIds.Except(existingGenreIds).Distinct().ToList();

            if (!newGenreIds.Any())
                return Result.Ok();

            var genres = await _unitOfWork.Genres.GetAllAsync(filter: g => newGenreIds.Contains(g.Id));
            if (genres.Count != newGenreIds.Count)
            {
                var missingGenres = newGenreIds.Except(genres.Items.Select(g => g.Id)).ToList();
                _logger.LogWarning("Genres with IDs: {MissingGenres} not found", string.Join(", ", missingGenres));
                return Result.Fail(GenreErrors.NotFound(missingGenres));
            }

            foreach (var genre in genres.Items)
            {
                book.BookGenres.Add(new BookGenre { BookId = book.Id, GenreId = genre.Id });
            }

            await _unitOfWork.SaveChangesAsync();
            return Result.Ok();
        }
    }
}
