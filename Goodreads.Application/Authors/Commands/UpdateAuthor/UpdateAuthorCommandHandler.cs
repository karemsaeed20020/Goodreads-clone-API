using Goodreads.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Management.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateAuthorCommandHandler> _logger;
        private readonly IImageService _imageService;

        public UpdateAuthorCommandHandler(
            IUnitOfWork unitOfWork,
            ILogger<UpdateAuthorCommandHandler> logger,
            IImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _imageService = imageService;
        }

        public async Task<Result> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            
                _logger.LogInformation("Updating author with ID: {AuthorId}", request.AuthorId);

                var author = await _unitOfWork.Authors.GetByIdAsync(request.AuthorId);
                if (author == null)
                {
                    return Result.Fail(AuthorErrors.NotFound(request.AuthorId));
                }

                // Update basic fields
                UpdateAuthorFields(author, request);

                // Handle profile picture update
                if (request.ProfilePicture != null)
                {
                    var pictureResult = await UpdateProfilePictureSafely(author, request.ProfilePicture);
                    if (!pictureResult.IsSuccess)
                    {
                        _logger.LogWarning("Profile picture update failed, but continuing with other updates");
                        // Continue with other updates even if picture fails
                    }
                }

                _unitOfWork.Authors.Update(author);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Author updated successfully: {AuthorId}", request.AuthorId);
                return Result.Ok();
            
            
        }

        private void UpdateAuthorFields(Author author, UpdateAuthorCommand request)
        {
            if (!string.IsNullOrEmpty(request.Name))
                author.Name = request.Name;

            if (!string.IsNullOrEmpty(request.Bio))
                author.Bio = request.Bio;
        }

        private async Task<Result> UpdateProfilePictureSafely(Author author, IFormFile profilePicture)
        {
            
                string? oldPublicId = author.ProfilePicturePublicId;
                string? newPublicId = null;

                // Upload new picture first (to avoid losing both if delete fails)
                using var stream = profilePicture.OpenReadStream();
                var (url, publicId) = await _imageService.UploadAsync(
                    profilePicture.FileName,
                    stream,
                    ImageContainer.Authors);

                newPublicId = publicId;
                var optimizedUrl = _imageService.GetProfilePictureUrl(publicId, 200);

                // Update author with new picture
                author.ProfilePictureUrl = optimizedUrl;
                author.ProfilePicturePublicId = newPublicId;

                // Delete old picture if exists (after successful upload)
                if (!string.IsNullOrEmpty(oldPublicId))
                {
                    await _imageService.DeleteAsync(oldPublicId);
                    _logger.LogInformation("Deleted old profile picture: {PublicId}", oldPublicId);
                }

                _logger.LogInformation("Profile picture updated successfully: {PublicId}", newPublicId);
                return Result.Ok();
            
            
        }
    }
}
