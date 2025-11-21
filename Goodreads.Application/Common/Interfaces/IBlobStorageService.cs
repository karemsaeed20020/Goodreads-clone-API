using Microsoft.Azure.Management.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Common.Interfaces
{
    public interface IBlobStorageService
    {
        Task<(string Url, string BlobName)> UploadAsync(string FileName, Stream Data, BlobContainer Container);
        string? GetUrl(string blobName);
        Task DeleteAsync(string blobName);
    }
}
