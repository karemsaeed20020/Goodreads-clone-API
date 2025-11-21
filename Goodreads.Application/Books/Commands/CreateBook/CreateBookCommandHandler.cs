using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Books.Commands.CreateBook
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateBookCommandHandler> _logger;
        private readonly IImageService _imageService;   
        public CreateBookCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateBookCommandHandler> logger, IImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _imageService = imageService;

        }
        public async Task<Result<string>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(request.AuthorId);
            if (author == null)
            {
                _logger.LogWarning("Author with ID {AuthorId} not found.", request.AuthorId);
                return Result<string>.Fail(AuthorErrors.NotFound(request.AuthorId));
            }
            var book = _mapper.Map<Book>(request);
            book.Author = author;
            if (request.CoverImage != null)
            {
                using var stream = request.CoverImage.OpenReadStream();
                var (url, publicId) = await _imageService.UploadAsync(
                    request.CoverImage.FileName,
                    stream,
                    ImageContainer.Books);
                book.CoverImageUrl = url;
                book.CoverImageBlobName = publicId;
            }
            await _unitOfWork.Books.AddAsync(book);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Book created successfully with ID: {BookId}", book.Id);
            return Result<string>.Ok(book.Id);
        }
    }
}
