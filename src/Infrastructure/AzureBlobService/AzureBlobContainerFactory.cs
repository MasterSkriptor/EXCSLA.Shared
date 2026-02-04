using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using EXCSLA.Shared.Infrastructure.Services.AzureBlobServices.Abstractions;

namespace EXCSLA.Shared.Infrastructure.Services;

public class AzureBlobContainerFactory : IAzureBlobContainerFactory
{
    private BlobServiceClient? _blobServiceClient;
    private BlobContainerClient? _blobContainerClient;
    private readonly string _containerName;
    private readonly string _connectionString;

    public AzureBlobContainerFactory(AzureBlobContainerFactoryOptions options)
    {
        _containerName = options.ContainerName;
        _connectionString = options.ConnectionString;
    }

    public async Task<BlobContainerClient> GetBlobContainer()
    {
        if (_blobContainerClient != null) return _blobContainerClient;

        var blobClient = GetBlobClient();
        _blobContainerClient = blobClient.GetBlobContainerClient(_containerName);
        _blobContainerClient ??= await blobClient.CreateBlobContainerAsync(_containerName);

        await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.Blob);

        return _blobContainerClient;
    }

    private BlobServiceClient GetBlobClient()
    {
        if (_blobServiceClient != null) return _blobServiceClient;

        _blobServiceClient = new BlobServiceClient(_connectionString);
        if (_blobServiceClient == null) throw new Exception("Failed to initialize azure blob storage account using present configuration.");

        return _blobServiceClient;
    }
}

public class AzureBlobContainerFactoryOptions
{
    public required string ContainerName { get; set; }
    public required string ConnectionString { get; set; }
}

public class AzureBlobService : IAzureBlobService
{
    private readonly IAzureBlobContainerFactory _blobContainerFactory;

    public AzureBlobService(IAzureBlobContainerFactory blobContainerFactory)
    {
        _blobContainerFactory = blobContainerFactory;
    }

    public async Task<bool> BlobExistsAsync(string blobName)
    {
        var blob = await GetBlobByNameAsync(blobName);
        return blob != null;
    }

    public async Task RemoveBlobAsync(string blobName)
    {
        var containerClient = await _blobContainerFactory.GetBlobContainer();
        await containerClient.DeleteBlobIfExistsAsync(blobName);
    }

    public async Task UploadBlobAsync(string blobName, Stream stream)
    {
        if (stream.Length > 0)
        {
            var containerClient = await _blobContainerFactory.GetBlobContainer();
            await containerClient.DeleteBlobIfExistsAsync(blobName);

            var blob = containerClient.GetBlobClient(blobName);

            using (stream)
            {
                await blob.UploadAsync(stream);
            }
        }
    }

    public async Task<BlobClient> GetBlobByNameAsync(string blobName)
    {
        var containerClient = await _blobContainerFactory.GetBlobContainer();
        return containerClient.GetBlobClient(blobName);
    }

    public async Task<List<BlobItem>> GetBlobList()
    {
        var containerClient = await _blobContainerFactory.GetBlobContainer();
        var blobList = new List<BlobItem>();

        await foreach (var item in containerClient.GetBlobsAsync())
        {
            blobList.Add(item);
        }

        return blobList;
    }
}
