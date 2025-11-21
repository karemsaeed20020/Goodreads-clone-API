using Goodreads.Application.Common.Interfaces;
using Microsoft.Azure.Management.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Authors.Commands.CreateAuthor
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateAuthorCommandHandler> _logger;
        private readonly IImageService _imageService; // ✅ Changed to IImageService
        private readonly IMapper _mapper;

        public CreateAuthorCommandHandler(
            IUnitOfWork unitOfWork,
            ILogger<CreateAuthorCommandHandler> logger,
            IImageService imageService, // ✅ Changed to IImageService
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _imageService = imageService; // ✅ Changed to IImageService
            _mapper = mapper;
        }

        public async Task<Result<string>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            
            
                _logger.LogInformation("Creating author: {Name}", request.Name);

                var author = _mapper.Map<Author>(request);

                // Upload profile picture to Cloudinary
                if (request.ProfilePicture != null && request.ProfilePicture.Length > 0)
                {
                    using var stream = request.ProfilePicture.OpenReadStream();
                    var (url, publicId) = await _imageService.UploadAsync(
                        request.ProfilePicture.FileName,
                        stream,
                        ImageContainer.Authors); // ✅ Use ImageContainer instead of BlobContainer

                    author.ProfilePictureUrl = url;
                    author.ProfilePicturePublicId = publicId; // ✅ Store publicId instead of blobName

                    _logger.LogInformation("Profile picture uploaded for author: {PublicId}", publicId);
                }

                await _unitOfWork.Authors.AddAsync(author);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Author created successfully with ID: {AuthorId}", author.Id);
                return Result<string>.Ok(author.Id);
            
            
        }
    }

}
