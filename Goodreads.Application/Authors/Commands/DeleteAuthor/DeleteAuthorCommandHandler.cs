using Goodreads.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Authors.Commands.DeleteAuhtor
{
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, Result> // ✅ Changed to public
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _imageService; // ✅ Changed to IImageService
        private readonly ILogger<DeleteAuthorCommandHandler> _logger;

        public DeleteAuthorCommandHandler(
            IUnitOfWork unitOfWork,
            IImageService imageService, // ✅ Changed to IImageService
            ILogger<DeleteAuthorCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _imageService = imageService; // ✅ Changed to IImageService
            _logger = logger;
        }

        public async Task<Result> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {

            var authorId = request.Id;
            _logger.LogInformation("Deleting author with ID: {AuthorId}", authorId);

            var author = await _unitOfWork.Authors.GetByIdAsync(authorId);
            if (author == null)
            {
                _logger.LogWarning("Author with ID: {AuthorId} not found", authorId);
                return Result.Fail(AuthorErrors.NotFound(authorId));
            }

            // ✅ Delete profile picture from Cloudinary if exists
            if (!string.IsNullOrEmpty(author.ProfilePicturePublicId))
            {
                await _imageService.DeleteAsync(author.ProfilePicturePublicId);
                _logger.LogInformation("✅ Deleted author profile picture from Cloudinary: {PublicId}", author.ProfilePicturePublicId);
            }
            else
            {
                _logger.LogInformation("ℹ️ No profile picture to delete for author: {AuthorId}", authorId);
            }

            _unitOfWork.Authors.Delete(author);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("✅ Author with ID: {AuthorId} deleted successfully", authorId);
            return Result.Ok();


        }
    }
}