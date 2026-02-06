using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace EXCSLA.Shared.Infrastructure.Services.AzureBlobServices.Abstractions;

/// <summary>
/// Service for managing blobs in Azure Blob Storage.
/// </summary>
/// <remarks>
/// <para>Provides high-level operations for uploading, downloading, deleting, and listing blobs.</para>
/// <para>All operations are asynchronous and interact with Azure Blob Storage via the container factory.</para>
/// </remarks>
public interface IAzureBlobService
{
    /// <summary>
    /// Checks if a blob exists in the container.
    /// </summary>
    /// <param name="blobName">The name of the blob to check.</param>
    /// <remarks>Returns true if the blob exists, false otherwise.</remarks>
    /// <returns>A task that represents the asynchronous operation. The task result contains true if the blob exists; otherwise, false.</returns>
    Task<bool> BlobExistsAsync(string blobName);
    
    /// <summary>
    /// Removes a blob from the container.
    /// </summary>
    /// <param name="blobName">The name of the blob to remove.</param>
    /// <remarks>
    /// <para>If the blob does not exist, this operation completes without error.</para>
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="Azure.RequestFailedException">Thrown if the Azure service request fails.</exception>
    Task RemoveBlobAsync(string blobName);
    
    /// <summary>
    /// Uploads a blob to the container, replacing existing blobs with the same name.
    /// </summary>
    /// <param name="blobName">The name of the blob to upload.</param>
    /// <param name="stream">The stream containing the blob data. Must be seekable.</param>
    /// <remarks>
    /// <para>If a blob with the same name exists, it will be deleted before uploading the new blob.</para>
    /// <para>The stream is disposed after upload completes.</para>
    /// <para>No action is taken if the stream is empty (length = 0).</para>
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="Azure.RequestFailedException">Thrown if the Azure service request fails.</exception>
    Task UploadBlobAsync(string blobName, Stream stream);
    
    /// <summary>
    /// Gets a blob client reference by name without downloading the blob.
    /// </summary>
    /// <param name="blobName">The name of the blob.</param>
    /// <remarks>
    /// <para>Returns a client reference that can be used for further blob operations.</para>
    /// <para>Does not require the blob to exist.</para>
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation. The task result contains the blob client.</returns>
    Task<BlobClient> GetBlobByNameAsync(string blobName);
    
    /// <summary>
    /// Gets a list of all blobs in the container.
    /// </summary>
    /// <remarks>
    /// <para>Returns metadata for all blobs without downloading blob content.</para>
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of blob items.</returns>
    /// <exception cref="Azure.RequestFailedException">Thrown if the Azure service request fails.</exception>
    Task<List<BlobItem>> GetBlobList();
}
