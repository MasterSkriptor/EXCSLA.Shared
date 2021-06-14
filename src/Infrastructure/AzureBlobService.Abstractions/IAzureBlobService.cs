using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace EXCSLA.Shared.Infrastructure.Services.AzureBlobServices.Abstractions
{
    public interface IAzureBlobService
    {
        Task<bool> BlobExistsAsync(string blobName);
        Task RemoveBlobAsync(string blobName);
        Task UploadBlobAsync(string blobName, Stream stream);
        Task<BlobClient> GetBlobByNameAsync(string blobName);
        Task<List<BlobItem>> GetBlobList();
    }
}
