
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using EXCSLA.Shared.Infrastructure.Services.AzureBlobServices.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace EXCSLA.Shared.Infrastructure.Services
{
    public class AzureBlobContainerFactory : IAzureBlobContainerFactory
    {
        private BlobServiceClient _blobServiceClient;
        private BlobContainerClient _blobContainerClient;
        private string _containerName;
        private string _connectionString;

        public AzureBlobContainerFactory(AzureBlobContainerFactoryOptions options)
        {
            _containerName = options.ContainerName;
            _connectionString = options.ConnectionString;
        }

        public async Task<BlobContainerClient> GetBlobContainer()
        {
            if (_blobContainerClient != null) return _blobContainerClient;


            // Create the container and return a container client object
            var blobClient = GetBlobClient();
            _blobContainerClient = blobClient.GetBlobContainerClient(_containerName);
            if (_blobContainerClient == null) _blobContainerClient = await blobClient.CreateBlobContainerAsync(_containerName);

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
        public string ContainerName { get; set; }
        public string ConnectionString { get; set; }
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

            if (blob != null) return true;

            return false;
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
                // Get BlobContainer
                var containerClient = await _blobContainerFactory.GetBlobContainer();

                // If blob exists delete the old one
                await containerClient.DeleteBlobIfExistsAsync(blobName);

                // Get a reference to a blob
                var blob = containerClient.GetBlobClient(blobName);

                // Open the file and upload its data
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
}
