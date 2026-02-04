using Azure.Storage.Blobs;

namespace EXCSLA.Shared.Infrastructure.Services.AzureBlobServices.Abstractions;

public interface IAzureBlobContainerFactory
{
    Task<BlobContainerClient> GetBlobContainer();
}
