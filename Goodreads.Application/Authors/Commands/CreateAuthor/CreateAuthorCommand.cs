using Goodreads.Application.Common.Attributes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Authors.Commands.CreateAuthor
{
    public class CreateAuthorCommand : IRequest<Result<string>>
    {
        public string Name { get; set; }
        public string Bio { get; set; }
        [AllowedExtensions(ExtensionGroup.Image)]
        [MaxFileSize(AppConstants.MaxFileSizeInBytes)]
        public IFormFile ProfilePicture { get; set; }
    }
}
