using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Goodreads.Application.Common.Interfaces;
using Microsoft.Azure.Management.Storage.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Infrastructure.Services.Storage
{
    public class CloudinaryImageService : IImageService
    {
        private readonly Cloudinary _cloudinary;
        private readonly ILogger<CloudinaryImageService> _logger;

        public CloudinaryImageService(IOptions<CloudinarySettings> options, ILogger<CloudinaryImageService> logger)
        {
            _logger = logger;
            var settings = options.Value;

            var account = new Account(settings.CloudName, settings.ApiKey, settings.ApiSecret);
            _cloudinary = new Cloudinary(account);
        }

        public async Task<(string Url, string PublicId)> UploadAsync(string fileName, Stream data, ImageContainer container)
        {
            try
            {
                var folder = GetFolderName(container);
                var publicId = $"{folder}/{Guid.NewGuid()}";

                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(fileName, data),
                    PublicId = publicId,
                    Transformation = GetDefaultTransformation(),
                    Folder = folder
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    _logger.LogInformation("✅ Image uploaded successfully: {PublicId}", publicId);
                    return (uploadResult.SecureUrl.ToString(), uploadResult.PublicId);
                }

                throw new Exception($"Upload failed: {uploadResult.Error?.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Failed to upload image: {FileName}", fileName);
                throw;
            }
        }

        public async Task DeleteAsync(string publicId)
        {
            try
            {
                var deleteParams = new DeletionParams(publicId);
                var result = await _cloudinary.DestroyAsync(deleteParams);

                if (result.Result == "ok")
                {
                    _logger.LogInformation("✅ Image deleted: {PublicId}", publicId);
                }
                else
                {
                    _logger.LogWarning("⚠️ Failed to delete image: {PublicId}", publicId);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error deleting image: {PublicId}", publicId);
                throw;
            }
        }

        public string? GetUrl(string publicId)
        {
            if (string.IsNullOrEmpty(publicId)) return null;

            return _cloudinary.Api.UrlImgUp
                .Transform(GetDefaultTransformation())
                .BuildUrl(publicId);
        }

        public string GetProfilePictureUrl(string publicId, int size = 200)
        {
            return _cloudinary.Api.UrlImgUp
                .Transform(new Transformation()
                    .Width(size)
                    .Height(size)
                    .Gravity("face")
                    .Crop("fill")
                    .Quality("auto")
                    .FetchFormat("auto"))
                .BuildUrl(publicId);
        }

        public string GetBookCoverUrl(string publicId, int width = 300, int height = 450)
        {
            return _cloudinary.Api.UrlImgUp
                .Transform(new Transformation()
                    .Width(width)
                    .Height(height)
                    .Crop("fill")
                    .Quality("auto")
                    .FetchFormat("auto"))
                .BuildUrl(publicId);
        }

        public string GetOptimizedUrl(string publicId, int? width = null, int? height = null)
        {
            var transformation = new Transformation()
                .Quality("auto")
                .FetchFormat("auto");

            if (width.HasValue) transformation = transformation.Width(width.Value);
            if (height.HasValue) transformation = transformation.Height(height.Value);

            return _cloudinary.Api.UrlImgUp
                .Transform(transformation)
                .BuildUrl(publicId);
        }

        private static Transformation GetDefaultTransformation()
        {
            return new Transformation()
                .Quality("auto")
                .FetchFormat("auto");
        }

        private string GetFolderName(ImageContainer container)
        {
            return container switch
            {
                ImageContainer.Users => "goodreads/users",
                ImageContainer.Authors => "goodreads/authors",
                ImageContainer.Books => "goodreads/books",
                ImageContainer.Others => "goodreads/others",
                _ => "goodreads/others"
            };
        }
    }
}

