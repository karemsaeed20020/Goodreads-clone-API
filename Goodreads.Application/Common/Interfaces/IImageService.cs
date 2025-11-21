using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Common.Interfaces
{
    public interface IImageService
    {
        Task<(string Url, string PublicId)> UploadAsync(string fileName, Stream data, ImageContainer container);
        Task DeleteAsync(string publicId);
        string? GetUrl(string publicId);
        string GetProfilePictureUrl(string publicId, int size = 200);
        string GetBookCoverUrl(string publicId, int width = 300, int height = 450);
    }

    public enum ImageContainer
    {
        Users,
        Authors,
        Books,
        Others
    }
}
