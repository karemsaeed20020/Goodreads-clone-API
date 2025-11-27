using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.ReadingProgresses.Commands.UpdateReadingProgress
{
    public class UpdateReadingProgressCommandHandler : IRequestHandler<UpdateReadingProgressCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;
        private readonly ILogger<UpdateReadingProgressCommandHandler> _logger;
        public UpdateReadingProgressCommandHandler(IUnitOfWork unitOfWork, IUserContext userContext,ILogger<UpdateReadingProgressCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _userContext = userContext;
            _logger = logger;
        }
        public async Task<Result> Handle(UpdateReadingProgressCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContext.UserId;
            if (userId == null)
            {
                return Result.Fail(AuthErrors.Unauthorized);
            }
            var progress = await _unitOfWork.ReadingPrgresses.GetSingleOrDefaultAsync(filter: rp => rp.UserId == userId && rp.BookId == request.BookId);
            var book = await _unitOfWork.Books.GetByIdAsync(request.BookId);
            if (book == null)
            {
                return Result.Fail(BookErrors.NotFound(request.BookId));
            }
            if (progress == null)
            {
                progress = new ReadingProgress
                {
                    UserId = userId,
                    BookId = request.BookId,
                    CurrentPage = request.CurrentPage,
                };
                await _unitOfWork.ReadingPrgresses.AddAsync(progress);
            } 
            else
            {
                progress.CurrentPage = request.CurrentPage; 
            }
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Updated reading progress for book {BookId} to page {Page}", request.BookId, request.CurrentPage);

            return Result.Ok();
        }
    }
}
