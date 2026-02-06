# EXCSLA.Shared.Infrastructure.Services.AzureBlobServices.Abstractions

Abstractions for Azure Blob Storage services in the EXCSLA framework.

## Overview

Provides interfaces and contracts for Azure Blob Storage operations without implementation details.

## Key Interface

- **IAzureBlobService** - Blob storage service contract

## Installation

```bash
dotnet add package EXCSLA.Shared.Infrastructure.Services.AzureBlobServices.Abstractions
```

## Features

- Clean abstraction for blob operations
- Dependency injection friendly
- Testable design
- Decoupled from Azure SDK specifics

## Dependencies

- .NET 10.0 or higher

## Implementation

For production implementation, use:
```bash
dotnet add package EXCSLA.Shared.Infrastructure.Services.AzureBlobService
```

## License

See LICENSE file in repository

## Support

For issues and questions, visit the [GitHub repository](https://github.com/MasterSkriptor/EXCSLA.Shared)
