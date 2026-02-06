using Azure.Storage.Blobs;

namespace EXCSLA.Shared.Infrastructure.Services.AzureBlobServices.Abstractions;

/// <summary>
/// Factory for creating and managing Azure Blob Storage container clients.
/// </summary>
/// <remarks>
/// <para>Provides lazy-loaded, cached access to Azure Blob Storage containers.</para>
/// <para>Automatically creates the container if it doesn't exist and sets public access policies.</para>
/// <para>
/// Example usage:
/// <code>
/// var factory = new AzureBlobContainerFactory(
///     new AzureBlobContainerFactoryOptions 
///     { 
///         ContainerName = "my-container",
///         ConnectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage")
///     }
/// );
/// var container = await factory.GetBlobContainer();
/// </code>
/// </para>
/// </remarks>
public interface IAzureBlobContainerFactory
{
    /// <summary>
    /// Gets or creates an Azure Blob Storage container.
    /// </summary>
    /// <remarks>
    /// <para>On first call, establishes connection and creates container if needed.</para>
    /// <para>Subsequent calls return the cached container client.</para>
    /// <para>Sets public access policy to allow public blob access.</para>
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation. The task result contains the blob container client.</returns>
    /// <exception cref="Azure.RequestFailedException">Thrown if the Azure service request fails.</exception>
    Task<BlobContainerClient> GetBlobContainer();
}
