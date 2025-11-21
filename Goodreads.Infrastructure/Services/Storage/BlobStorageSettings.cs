using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Infrastructure.Services.Storage
{
    public class BlobStorageSettings
    {
        public const string Section = "BlobStorageSettings";
        public string ConnectionString { get; set; } = default!;
        public string ContainerName { get; set; } = default!;
        public string AccountName { get; set; } = default!;
        public string AccountKey { get; set; } = default!;
    }
}
