using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using EXCSLA.Shared.Infrastructure.Services.AzureBlobServices.Abstractions;

namespace EXCSLA.Shared.Infrastructure.Services;

/// <summary>
/// Factory implementation for creating and managing Azure Blob Storage container clients.
/// </summary>
/// <remarks>
/// <para>Provides lazy-initialized, cached access to Azure Blob Storage containers.</para>
/// <para>Manages lifecycle of blob service and container clients with thread-safe lazy initialization.</para>
/// </remarks>
public class AzureBlobContainerFactory : IAzureBlobContainerFactory
{
    private BlobServiceClient? _blobServiceClient;
    private BlobContainerClient? _blobContainerClient;
    private readonly string _containerName;
    private readonly string _connectionString;

    /// <summary>
    /// Initializes a new instance of the <see cref="AzureBlobContainerFactory"/> class.
    /// </summary>
    /// <param name="options">Configuration options containing container name and connection string.</param>
    /// <remarks>
    /// <para>The constructor validates that options is not null via the DI framework.</para>
    /// </remarks>
    public AzureBlobContainerFactory(AzureBlobContainerFactoryOptions options)
    {
        _containerName = options.ContainerName;
        _connectionString = options.ConnectionString;
    }

    /// <summary>
    /// Gets or creates an Azure Blob Storage container asynchronously.
    /// </summary>
    /// <remarks>
    /// <para>On first invocation:</para>
    /// <list type="bullet">
    /// <item>Creates a BlobServiceClient using the connection string</item>
    /// <item>Gets a reference to the named container</item>
    /// <item>Creates the container if it doesn't exist</item>
    /// <item>Sets the container access policy to allow public blob access</item>
    /// </list>
    /// <para>Subsequent invocations return the cached container client.</para>
    /// </remarks>
    /// <returns>The blob container client for the configured container.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the BlobServiceClient fails to initialize.</exception>
    /// <exception cref="Azure.RequestFailedException">Thrown if the Azure service request fails.</exception>
    public async Task<BlobContainerClient> GetBlobContainer()
    {
        if (_blobContainerClient != null) return _blobContainerClient;

        var blobClient = GetBlobClient();
        _blobContainerClient = blobClient.GetBlobContainerClient(_containerName);
        _blobContainerClient ??= await blobClient.CreateBlobContainerAsync(_containerName);

        await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.Blob);

        return _blobContainerClient;
    }

    /// <summary>
    /// Gets or creates a BlobServiceClient for the Azure Blob Storage account.
    /// </summary>
    /// <remarks>
    /// <para>Uses lazy initialization to create the client only when first needed.</para>
    /// <para>The same client instance is reused for all subsequent operations.</para>
    /// </remarks>
    /// <returns>The blob service client.</returns>
    /// <exception cref="InvalidOperationException">Thrown if client initialization fails.</exception>
    private BlobServiceClient GetBlobClient()
    {
        if (_blobServiceClient != null) return _blobServiceClient;

        _blobServiceClient = new BlobServiceClient(_connectionString);
        if (_blobServiceClient == null) throw new InvalidOperationException("Failed to initialize Azure Blob Storage account. Verify the connection string is valid.");

        return _blobServiceClient;
    }
}

/// <summary>
/// Configuration options for <see cref="AzureBlobContainerFactory"/>.
/// </summary>
/// <remarks>
/// <para>Contains essential configuration for connecting to Azure Blob Storage.</para>
/// </remarks>
public class AzureBlobContainerFactoryOptions
{
    /// <summary>
    /// Gets or sets the name of the blob container.
    /// </summary>
    /// <remarks>
    /// <para>Must be a valid Azure Blob Storage container name (3-63 characters, lowercase alphanumeric and hyphens).</para>
    /// </remarks>
    public required string ContainerName { get; set; }
    
    /// <summary>
    /// Gets or sets the Azure Storage connection string.
    /// </summary>
    /// <remarks>
    /// <para>Should be in format: DefaultEndpointsProtocol=https;AccountName=..;AccountKey=..;EndpointSuffix=core.windows.net</para>
    /// </remarks>
    public required string ConnectionString { get; set; }
}

/// <summary>
/// Service for managing blobs in Azure Blob Storage.
/// </summary>
/// <remarks>
/// <para>Provides high-level operations for blob lifecycle management.</para>
/// <para>All operations delegate to the blob container obtained from the factory.</para>
/// </remarks>
public class AzureBlobService : IAzureBlobService
{
    private readonly IAzureBlobContainerFactory _blobContainerFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="AzureBlobService"/> class.
    /// </summary>
    /// <param name="blobContainerFactory">The blob container factory for obtaining container clients.</param>
    /// <remarks>
    /// <para>The factory is injected and used for all blob operations.</para>
    /// </remarks>
    public AzureBlobService(IAzureBlobContainerFactory blobContainerFactory)
    {
        _blobContainerFactory = blobContainerFactory;
    }

    /// <summary>
    /// Checks if a blob exists in the container.
    /// </summary>
    /// <param name="blobName">The name of the blob to check.</param>
    /// <returns>True if the blob exists; otherwise, false.</returns>
    public async Task<bool> BlobExistsAsync(string blobName)
    {
        var blob = await GetBlobByNameAsync(blobName);
        return blob != null;
    }

    /// <summary>
    /// Removes a blob from the container.
    /// </summary>
    /// <param name="blobName">The name of the blob to remove.</param>
    /// <remarks>
    /// <para>If the blob does not exist, the operation completes without error.</para>
    /// </remarks>
    public async Task RemoveBlobAsync(string blobName)
    {
        var containerClient = await _blobContainerFactory.GetBlobContainer();
        await containerClient.DeleteBlobIfExistsAsync(blobName);
    }

    /// <summary>
    /// Uploads a blob to the container, replacing any existing blob with the same name.
    /// </summary>
    /// <param name="blobName">The name of the blob to upload.</param>
    /// <param name="stream">The stream containing the blob data.</param>
    /// <remarks>
    /// <para>If a blob with the same name exists, it will be deleted before uploading.</para>
    /// <para>The stream is disposed after upload completes.</para>
    /// <para>If the stream is empty (length = 0), the operation is skipped.</para>
    /// </remarks>
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

    /// <summary>
    /// Gets a blob client reference by name.
    /// </summary>
    /// <param name="blobName">The name of the blob.</param>
    /// <remarks>
    /// <para>Returns a client reference for further blob operations.</para>
    /// <para>Does not require the blob to exist.</para>
    /// </remarks>
    /// <returns>A blob client for the specified blob name.</returns>
    public async Task<BlobClient> GetBlobByNameAsync(string blobName)
    {
        var containerClient = await _blobContainerFactory.GetBlobContainer();
        return containerClient.GetBlobClient(blobName);
    }

    /// <summary>
    /// Gets a list of all blobs in the container.
    /// </summary>
    /// <remarks>
    /// <para>Returns metadata for all blobs without downloading blob content.</para>
    /// </remarks>
    /// <returns>A list of blob items in the container.</returns>
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
