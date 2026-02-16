# Azure Blob Storage Service

A thin, production-ready wrapper around Azure Blob Storage for managing files (images, PDFs, documents, etc.) with built-in lazy initialization, caching, and optimized overwrite handling.

## Table of Contents

- [Overview](#overview)
- [Installation](#installation)
- [Configuration](#configuration)
- [Usage](#usage)
- [API Reference](#api-reference)
- [Best Practices](#best-practices)
- [Architecture](#architecture)

## Overview

The `AzureBlobService` provides a simplified interface to Azure Blob Storage that:

- **Lazy-initializes** blob container clients on first use
- **Caches** container clients for performance
- **Manages** file uploads, downloads, deletion, and listing
- **Handles** empty streams intelligently (skips upload)
- **Implements** delete-before-upload pattern to prevent file versioning issues
- **Sets public access** policies for blob retrieval
- **Supports** any file type (images, PDFs, archives, etc.)

## Installation

### Prerequisites

- .NET 10.0 or later
- Azure Storage account
- Azure.Storage.Blobs NuGet package (v12.20.0+)

### NuGet Installation

```bash
dotnet add package Azure.Storage.Blobs --version 12.20.0
```

The service is included in the `EXCSLA.Shared.Infrastructure.Services.AzureBlobService` package.

## Configuration

### Setup in Dependency Injection Container

Register the service in your `Program.cs` or dependency injection configuration:

```csharp
using EXCSLA.Shared.Infrastructure.Services;
using EXCSLA.Shared.Infrastructure.Services.AzureBlobServices.Abstractions;

// In your DI setup
var services = new ServiceCollection();

// Configure Azure Blob Storage options
var options = new AzureBlobContainerFactory.AzureBlobContainerFactoryOptions
{
    ConnectionString = "DefaultEndpointsProtocol=https;AccountName=youraccountname;AccountKey=...;EndpointSuffix=domain.windows.net",
    ContainerName = "your-container-name"
};

// Register the factory
services.AddSingleton(options);
services.AddSingleton<IAzureBlobContainerFactory, AzureBlobContainerFactory>();

// Register the service
services.AddSingleton<IAzureBlobService, AzureBlobService>();

var provider = services.BuildServiceProvider();
```

### Environment Configuration

Store your connection string in environment variables or secure configuration:

```csharp
// From environment variables
var connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage")
    ?? throw new InvalidOperationException("AzureWebJobsStorage not configured");

var options = new AzureBlobContainerFactory.AzureBlobContainerFactoryOptions
{
    ConnectionString = connectionString,
    ContainerName = "application-files"
};
```

## Usage

### Inject and Use the Service

```csharp
public class FileStorageService
{
    private readonly IAzureBlobService _blobService;

    public FileStorageService(IAzureBlobService blobService)
    {
        _blobService = blobService;
    }

    // Your implementation...
}
```

### Upload a File

```csharp
// Upload a user profile photo
using var photoStream = new FileStream("profile.jpg", FileMode.Open);
await _blobService.UploadBlobAsync("user-profile-12345.jpg", photoStream);

// The service automatically:
// 1. Checks if stream is not empty (skips empty files)
// 2. Deletes any existing file with the same name
// 3. Uploads the new file
// 4. Sets public access policy
```

### Check if a File Exists

```csharp
bool exists = await _blobService.BlobExistsAsync("user-profile-12345.jpg");

if (exists)
{
    Console.WriteLine("Profile photo already uploaded");
}
else
{
    Console.WriteLine("No profile photo found");
}
```

### Download a File

```csharp
// Get a blob client reference for direct access
var blobClient = await _blobService.GetBlobByNameAsync("document.pdf");

if (blobClient != null)
{
    // Download directly
    var download = await blobClient.DownloadAsync();
    using (var file = File.OpenWrite("downloaded.pdf"))
    {
        await download.Value.Content.CopyToAsync(file);
    }
}
```

### Delete a File

```csharp
// Remove a file
await _blobService.RemoveBlobAsync("old-document.pdf");

// The service gracefully handles non-existent files
// Operation completes without error if file doesn't exist
```

### List All Files in Container

```csharp
// Get all blobs
var blobs = await _blobService.GetBlobList();

foreach (var blob in blobs)
{
    Console.WriteLine($"File: {blob.Name}, Size: {blob.Properties.ContentLength}");
}

// Output example:
// File: profile-photo.jpg, Size: 245000
// File: resume.pdf, Size: 512000
// File: contract.docx, Size: 1024000
```

## API Reference

### IAzureBlobService Interface

#### `BlobExistsAsync(string blobName)`

Checks if a blob exists in the container.

**Parameters:**
- `blobName` (string) - Name/path of the blob to check

**Returns:** Task<bool> - True if blob exists, false otherwise

**Example:**
```csharp
var exists = await _blobService.BlobExistsAsync("myfile.txt");
```

---

#### `UploadBlobAsync(string blobName, Stream content)`

Uploads or overwrites a blob in the container.

**Parameters:**
- `blobName` (string) - Name/path for the blob
- `content` (Stream) - File content stream

**Behavior:**
- Skips upload if stream is empty (length == 0)
- Deletes existing blob with same name before uploading
- Sets public blob access policy after upload
- Supports files of any size

**Example:**
```csharp
using var fileStream = new FileStream("document.pdf", FileMode.Open);
await _blobService.UploadBlobAsync("documents/my-document.pdf", fileStream);
```

---

#### `RemoveBlobAsync(string blobName)`

Removes a blob from the container.

**Parameters:**
- `blobName` (string) - Name/path of the blob to delete

**Behavior:**
- Completes without error if blob doesn't exist
- Graceful deletion handling

**Example:**
```csharp
await _blobService.RemoveBlobAsync("archive/old-file.zip");
```

---

#### `GetBlobByNameAsync(string blobName)`

Retrieves a blob client reference for direct access.

**Parameters:**
- `blobName` (string) - Name/path of the blob

**Returns:** Task<BlobClient> - Client for direct Azure SDK operations

**Example:**
```csharp
var blob = await _blobService.GetBlobByNameAsync("image.jpg");
if (blob != null)
{
    var properties = await blob.GetPropertiesAsync();
    Console.WriteLine($"Size: {properties.Value.ContentLength}");
}
```

---

#### `GetBlobList()`

Retrieves a list of all blobs in the container.

**Returns:** Task<List<BlobItem>> - Collection of blob metadata

**Example:**
```csharp
var blobs = await _blobService.GetBlobList();
Console.WriteLine($"Total blobs: {blobs.Count}");
```

### IAzureBlobContainerFactory Interface

#### `GetBlobContainer()`

Gets or creates the blob container client (called internally by the service).

**Returns:** Task<BlobContainerClient>

**Behavior:**
- Returns cached client on subsequent calls
- Creates container on first call if it doesn't exist
- Sets public access policy

## Best Practices

### 1. Stream Management

Always use `using` statements to ensure streams are properly disposed:

```csharp
using var fileStream = System.IO.File.OpenRead("large-file.pdf");
await _blobService.UploadBlobAsync("files/document.pdf", fileStream);
// Stream automatically disposed after upload
```

### 2. Empty Stream Handling

The service automatically skips uploads for empty streams:

```csharp
// This is safe - empty streams won't cause issues
using var stream = new MemoryStream(); // Empty by default
await _blobService.UploadBlobAsync("test.txt", stream);
// Upload is skipped, factory not called
```

### 3. File Naming Conventions

Use hierarchical naming for organization:

```csharp
// Good: Organized by category and identifier
await _blobService.UploadBlobAsync($"users/{userId}/profile.jpg", stream);
await _blobService.UploadBlobAsync($"documents/{orderId}/invoice.pdf", stream);

// Good: Timestamp-based versioning (use blob names, not blob versioning)
var filename = $"exports/report-{DateTime.UtcNow:yyyy-MM-dd-HHmmss}.xlsx";
await _blobService.UploadBlobAsync(filename, stream);
```

### 4. Overwrite Pattern

The service implements a delete-before-upload pattern to prevent versioning issues:

```csharp
// Safe to call multiple times with same name
// Old file deleted, new file uploaded
await _blobService.UploadBlobAsync("profile/avatar.jpg", newPhoto);
```

### 5. Error Handling

Handle Azure exceptions appropriately:

```csharp
try
{
    await _blobService.UploadBlobAsync("file.pdf", fileStream);
}
catch (Azure.RequestFailedException ex) when (ex.Status == 403)
{
    // Authentication/Authorization failure
    Console.WriteLine("Access denied to blob storage");
}
catch (Azure.RequestFailedException ex)
{
    // Other Azure service failures
    Console.WriteLine($"Blob storage error: {ex.Message}");
}
```

### 6. Supported File Types

The service supports any file type. Common use cases:

```csharp
// Images
await _blobService.UploadBlobAsync($"images/{id}.jpg", jpgStream);
await _blobService.UploadBlobAsync($"images/{id}.png", pngStream);

// Documents
await _blobService.UploadBlobAsync($"documents/{id}.pdf", pdfStream);
await _blobService.UploadBlobAsync($"documents/{id}.docx", docxStream);

// Data files
await _blobService.UploadBlobAsync($"exports/{id}.xlsx", excelStream);
await _blobService.UploadBlobAsync($"exports/{id}.csv", csvStream);

// Archives
await _blobService.UploadBlobAsync($"backups/{id}.zip", zipStream);
```

## Architecture

### Lazy Initialization Pattern

The service and factory use lazy initialization to improve startup time:

```
First Access (Lazy Initialization):
1. Service calls GetBlobContainer()
2. Factory creates BlobServiceClient (once)
3. Factory gets container reference
4. Factory creates container if missing
5. Factory sets public access policy
6. Container client cached for reuse

Subsequent Accesses:
1. Cached container client returned immediately
2. No network round-trips
```

### Delete-Before-Upload Pattern

The service prevents file versioning issues by deleting before uploading:

```
UploadBlobAsync("myfile.txt", stream):
1. Check if stream is empty
   → If empty, return early (no upload)
2. Delete existing blob if present
3. Upload new blob content
4. Transaction-like behavior prevents orphaned versions
```

### Container Access Policy

Public blob access is set automatically for public files:

```csharp
// Automatically called by factory on first initialization
containerClient.SetAccessPolicyAsync(PublicAccessType.Blob);

// Result: Uploaded blobs can be accessed via public URL
// https://myaccount.blob.domain.windows.net/mycontainer/file.jpg
```

## Common Scenarios

### Handling User File Uploads

```csharp
public async Task<UploadResult> UploadUserDocument(int userId, IFormFile file)
{
    if (file == null || file.Length == 0)
        return new UploadResult { Success = false, Message = "File is empty" };

    var documentId = Guid.NewGuid().ToString();
    var blobName = $"users/{userId}/documents/{documentId}.{GetExtension(file.FileName)}";

    using (var stream = file.OpenReadStream())
    {
        try
        {
            await _blobService.UploadBlobAsync(blobName, stream);
            return new UploadResult 
            { 
                Success = true, 
                DocumentId = documentId,
                Url = GetPublicUrl(blobName)
            };
        }
        catch (Exception ex)
        {
            return new UploadResult { Success = false, Message = ex.Message };
        }
    }
}
```

### Listing Files for a User

```csharp
public async Task<List<UserFile>> GetUserFiles(int userId)
{
    var allBlobs = await _blobService.GetBlobList();
    var userPrefix = $"users/{userId}/";

    var userFiles = allBlobs
        .Where(b => b.Name.StartsWith(userPrefix))
        .Select(b => new UserFile
        {
            Name = b.Name.Substring(userPrefix.Length),
            Size = b.Properties.ContentLength ?? 0,
            UploadDate = b.Properties.CreatedOn?.UtcDateTime ?? DateTime.MinValue
        })
        .ToList();

    return userFiles;
}
```

### Cleanup Operations

```csharp
public async Task DeleteUserFiles(int userId)
{
    var userPrefix = $"users/{userId}/";
    var allBlobs = await _blobService.GetBlobList();

    var filesToDelete = allBlobs
        .Where(b => b.Name.StartsWith(userPrefix))
        .Select(b => b.Name)
        .ToList();

    foreach (var blobName in filesToDelete)
    {
        await _blobService.RemoveBlobAsync(blobName);
    }

    Console.WriteLine($"Deleted {filesToDelete.Count} files for user {userId}");
}
```

## Troubleshooting

### Connection String Issues

```
Error: "The string is not in the correct format"
→ Verify connection string format:
    DefaultEndpointsProtocol=https;AccountName=...;AccountKey=...;EndpointSuffix=domain.windows.net
```

### Container Name Validation

```
Error: "Container name cannot be null or whitespace"
→ Container names must be:
  - 3-63 characters long
  - Lowercase letters, numbers, hyphens only
  - Start and end with letter or number
```

### Authentication Failures

```
Error: "AuthenticationFailed" or "403 Forbidden"
→ Check:
  1. Connection string is correct
  2. Azure Storage account exists
  3. Service principal has necessary permissions
```

### Large File Performance

For files > 100MB, consider:

```csharp
// Upload in chunks if needed
// Or use Azure Storage's built-in streaming capabilities
using var fileStream = File.OpenRead("large-file.bin");
await _blobService.UploadBlobAsync("large-files/data.bin", fileStream);
// Service handles streaming automatically
```

## Related Documentation

- [Azure Blob Storage Documentation](https://learn.microsoft.com/en-us/azure/storage/blobs/)
- [Azure SDK for .NET - Blobs](https://learn.microsoft.com/en-us/dotnet/api/overview/azure/storage.blobs-readme)
- [EXCSLA.Shared Framework Documentation](../../../README.md)

## License

Part of the EXCSLA.Shared Domain-Driven Design Framework.
