using Goodreads.Application.Common.Attributes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand : IRequest<Result>
    {
        public string AuthorId { get; set; }
        public string? Name { get; set; }
        public string? Bio { get; set; }
        [AllowedExtensions(ExtensionGroup.Image)]
        [MaxFileSize(AppConstants.MaxFileSizeInBytes)]
        public IFormFile? ProfilePicture { get; set; }
    }
}
