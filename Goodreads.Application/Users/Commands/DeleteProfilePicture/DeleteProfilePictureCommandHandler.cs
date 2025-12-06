using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Users.Commands.DeleteProfilePicture
{
    internal class DeleteProfilePictureCommandHandler : IRequestHandler<DeleteProfilePictureCommand, Result>
    {
        private readonly ILogger<DeleteProfilePictureCommandHandler> _logger;
        private readonly IUserContext _userContext;
        private readonly UserManager<User> _userManager;
        private readonly IImageService _imageService; // Use IImageService

        public DeleteProfilePictureCommandHandler(
            ILogger<DeleteProfilePictureCommandHandler> logger,
            IUserContext userContext,
            UserManager<User> userManager,
            IImageService imageService) // Changed parameter
        {
            _logger = logger;
            _userContext = userContext;
            _userManager = userManager;
            _imageService = imageService;
        }

        public async Task<Result> Handle(DeleteProfilePictureCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContext.UserId;
            if (userId == null)
                return Result.Fail(AuthErrors.Unauthorized);

            _logger.LogInformation("Deleting Profile Picture for user with Id : {UserId}", userId);

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                _logger.LogWarning("User not found: {UserId}", userId);
                return Result.Fail(UserErrors.NotFound(userId));
            }

            // Use ProfilePictureBlobName as Cloudinary Public ID
            if (!string.IsNullOrEmpty(user.ProfilePictureBlobName))
            {
                try
                {
                    await _imageService.DeleteAsync(user.ProfilePictureBlobName);
                    _logger.LogInformation("Successfully deleted Cloudinary image: {PublicId}", user.ProfilePictureBlobName);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to delete Cloudinary image: {PublicId}", user.ProfilePictureBlobName);
                    // You might want to decide whether to continue or fail here
                    // For now, let's continue and clear the fields anyway
                }
            }

            // Clear profile picture fields
            user.ProfilePictureBlobName = null; // This stores Cloudinary public ID
            user.ProfilePictureUrl = null;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                _logger.LogError("Failed to update profile picture for user: {UserId}. Errors: {Errors}", userId, result.Errors);
                return Result.Fail(UserErrors.UpdateFailed(userId));
            }

            _logger.LogInformation("Profile picture removed for user: {UserId}", userId);
            return Result.Ok();
        }
    }
}
