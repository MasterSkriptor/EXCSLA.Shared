using Azure.Storage.Blobs;
using System.Threading.Tasks;

namespace EXCSLA.Shared.Infrastructure.Services.AzureBlobServices.Abstractions
{
    public interface IAzureBlobContainerFactory
    {
        Task<BlobContainerClient> GetBlobContainer();
    }
}
