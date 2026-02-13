# EXCSLA.Shared Repository Migration Plan - Version 5.2.1

**Target:** Bump all EXCSLA.Shared.* packages to v5.2.1 (.NET 10 certification)  
**Repository:** https://github.com/MasterSkriptor/EXCSLA.Shared  
**Expected Outcome:** All 8 packages published to NuGet.org as appropriate versions  
**Timeline:** Single iteration via agent execution

---

## Overview

This plan provides detailed steps to update the EXCSLA.Shared repository for .NET 10 certification and publish all packages:
- **7 Core packages → v5.2.1**
- **SendGridEmailService → v5.3.0** (unchanged, already compatible)

After completion, the EXCSLA main project will convert ProjectReferences to PackageReferences.

---

## Prerequisites

- Access to EXCSLA.Shared GitHub repository (https://github.com/MasterSkriptor/EXCSLA.Shared)
- .NET 10.0 SDK installed (`dotnet --version`)
- NuGet API key with publish permissions
- Write permissions to EXCSLA.Shared repository

---

## Step 1: Clone and Explore Repository

```bash
git clone https://github.com/MasterSkriptor/EXCSLA.Shared.git
cd EXCSLA.Shared

# Verify structure
find . -name "*.csproj" -type f | grep -v bin | grep -v obj | sort
```

**Expected projects:**
- src/Core/Abstractions/Abstractions.csproj
- src/Core/DomainModels/DomainModels.csproj
- src/Core/ValueObjects/ValueObjects.csproj
- src/Core/Specifications/Specifications.csproj
- src/Core/Exceptions/Exceptions.csproj
- src/Core/GuardClauses/GuardClauses.csproj
- src/Core/Abstractions.DomainEventDispatcher/Abstractions.DomainEventDispatcher.csproj
- src/Infrastructure/SendGridEmailService/SendGridEmailService.csproj

---

## Step 2: Update Versions to 5.2.1 (All Core Packages)

Update these 7 projects from 5.2.0 → 5.2.1:

```bash
# Abstractions.csproj
sed -i 's/<Version>5.2.0<\/Version>/<Version>5.2.1<\/Version>/g' src/Core/Abstractions/Abstractions.csproj

# DomainModels.csproj
sed -i 's/<Version>5.2.0<\/Version>/<Version>5.2.1<\/Version>/g' src/Core/DomainModels/DomainModels.csproj

# ValueObjects.csproj
sed -i 's/<Version>5.2.0<\/Version>/<Version>5.2.1<\/Version>/g' src/Core/ValueObjects/ValueObjects.csproj

# Specifications.csproj
sed -i 's/<Version>5.2.0<\/Version>/<Version>5.2.1<\/Version>/g' src/Core/Specifications/Specifications.csproj

# Exceptions.csproj
sed -i 's/<Version>5.2.0<\/Version>/<Version>5.2.1<\/Version>/g' src/Core/Exceptions/Exceptions.csproj

# GuardClauses.csproj
sed -i 's/<Version>5.2.0<\/Version>/<Version>5.2.1<\/Version>/g' src/Core/GuardClauses/GuardClauses.csproj

# Abstractions.DomainEventDispatcher.csproj
sed -i 's/<Version>5.2.0<\/Version>/<Version>5.2.1<\/Version>/g' src/Core/Abstractions.DomainEventDispatcher/Abstractions.DomainEventDispatcher.csproj

# Verify SendGridEmailService is still 5.3.0
grep "<Version>" src/Infrastructure/SendGridEmailService/SendGridEmailService.csproj
```

**Verify changes:**
```bash
echo "=== Verifying Versions ==="
for proj in src/Core/*/Abstractions.csproj src/Core/DomainModels/DomainModels.csproj src/Core/ValueObjects/ValueObjects.csproj src/Core/Specifications/Specifications.csproj src/Core/Exceptions/Exceptions.csproj src/Core/GuardClauses/GuardClauses.csproj src/Core/Abstractions.DomainEventDispatcher/Abstractions.DomainEventDispatcher.csproj; do
  if [ -f "$proj" ]; then
    echo "$proj: $(grep -oP '<Version>\K[^<]+' "$proj")"
  fi
done
```

---

## Step 3: Verify .NET 10 Framework Settings

Ensure all projects have:

```bash
# Check TargetFramework = net10.0
echo "=== TargetFrameworks ==="
grep -r "<TargetFramework>" src/ | grep -v bin | grep -v obj

# Check ImplicitUsings = enable
echo "=== ImplicitUsings ==="
grep -r "<ImplicitUsings>" src/ | grep -v bin | grep -v obj

# Check Nullable = enable  
echo "=== Nullable ==="
grep -r "<Nullable>" src/ | grep -v bin | grep -v obj

# Check LangVersion = latest
echo "=== LangVersion ==="
grep -r "<LangVersion>" src/ | grep -v bin | grep -v obj
```

All should show "enable" or "latest" and "net10.0". If missing, add to PropertyGroup in .csproj files.

---

## Step 4: Update Package Descriptions (Recommended)

Update descriptions to reference .NET 10 support. For each project file, update the `<Description>`:

```bash
# Abstractions - if it has a Description tag
sed -i 's/Description for Abstractions/Core abstractions for domain-driven design (.NET 10 support)/g' src/Core/Abstractions/Abstractions.csproj

# DomainModels
sed -i 's/Description for DomainModels/Base domain classes for domain-driven design (.NET 10 support)/g' src/Core/DomainModels/DomainModels.csproj

# ValueObjects
sed -i 's/Description for ValueObjects/Reusable value objects for domain-driven design (.NET 10 support)/g' src/Core/ValueObjects/ValueObjects.csproj

# Specifications
sed -i 's/Description for Specifications/Specification pattern implementation for DDD queries (.NET 10 support)/g' src/Core/Specifications/Specifications.csproj

# Exceptions
sed -i 's/Description for Exceptions/Domain-specific exceptions (.NET 10 support)/g' src/Core/Exceptions/Exceptions.csproj

# GuardClauses
sed -i 's/Description for GuardClauses/Extended guard clauses for validation (.NET 10 support)/g' src/Core/GuardClauses/GuardClauses.csproj

# Abstractions.DomainEventDispatcher
sed -i 's/Description for DomainEventDispatcher/Domain event dispatcher abstractions (.NET 10 support)/g' src/Core/Abstractions.DomainEventDispatcher/Abstractions.DomainEventDispatcher.csproj
```

---

## Step 5: Build and Test

```bash
# Clean previous builds
dotnet clean

# Restore
dotnet restore

# Build
dotnet build

# Expected: Build succeeded with 0 errors, 0 warnings
```

---

## Step 6: Create NuGet Packages

```bash
# Pack all projects
dotnet pack src/Core/Abstractions/Abstractions.csproj --no-build --configuration Release
dotnet pack src/Core/DomainModels/DomainModels.csproj --no-build --configuration Release
dotnet pack src/Core/ValueObjects/ValueObjects.csproj --no-build --configuration Release
dotnet pack src/Core/Specifications/Specifications.csproj --no-build --configuration Release
dotnet pack src/Core/Exceptions/Exceptions.csproj --no-build --configuration Release
dotnet pack src/Core/GuardClauses/GuardClauses.csproj --no-build --configuration Release
dotnet pack src/Core/Abstractions.DomainEventDispatcher/Abstractions.DomainEventDispatcher.csproj --no-build --configuration Release
dotnet pack src/Infrastructure/SendGridEmailService/SendGridEmailService.csproj --no-build --configuration Release

# Verify .nupkg files
find . -name "*.nupkg" -type f
```

**Expected .nupkg files:**
- EXCSLA.Shared.Core.Abstractions.5.2.1.nupkg
- EXCSLA.Shared.Core.5.2.1.nupkg
- EXCSLA.Shared.Core.ValueObjects.5.2.1.nupkg
- EXCSLA.Shared.Core.Specifications.5.2.1.nupkg
- EXCSLA.Shared.Core.Exceptions.5.2.1.nupkg
- EXCSLA.Shared.Core.GuardClauses.5.2.1.nupkg
- EXCSLA.Shared.Core.Abstractions.DomainEventDispatcher.5.2.1.nupkg
- EXCSLA.Shared.Infrastructure.SendGridEmailService.5.3.0.nupkg

---

## Step 7: Publish to NuGet.org

```bash
# Set your API key
export NUGET_API_KEY="your-api-key-from-nuget.org"

# Push packages
dotnet nuget push "src/Core/Abstractions/bin/Release/EXCSLA.Shared.Core.Abstractions.5.2.1.nupkg" --api-key $NUGET_API_KEY --source https://api.nuget.org/v3/index.json

dotnet nuget push "src/Core/DomainModels/bin/Release/EXCSLA.Shared.Core.5.2.1.nupkg" --api-key $NUGET_API_KEY --source https://api.nuget.org/v3/index.json

dotnet nuget push "src/Core/ValueObjects/bin/Release/EXCSLA.Shared.Core.ValueObjects.5.2.1.nupkg" --api-key $NUGET_API_KEY --source https://api.nuget.org/v3/index.json

dotnet nuget push "src/Core/Specifications/bin/Release/EXCSLA.Shared.Core.Specifications.5.2.1.nupkg" --api-key $NUGET_API_KEY --source https://api.nuget.org/v3/index.json

dotnet nuget push "src/Core/Exceptions/bin/Release/EXCSLA.Shared.Core.Exceptions.5.2.1.nupkg" --api-key $NUGET_API_KEY --source https://api.nuget.org/v3/index.json

dotnet nuget push "src/Core/GuardClauses/bin/Release/EXCSLA.Shared.Core.GuardClauses.5.2.1.nupkg" --api-key $NUGET_API_KEY --source https://api.nuget.org/v3/index.json

dotnet nuget push "src/Core/Abstractions.DomainEventDispatcher/bin/Release/EXCSLA.Shared.Core.Abstractions.DomainEventDispatcher.5.2.1.nupkg" --api-key $NUGET_API_KEY --source https://api.nuget.org/v3/index.json

dotnet nuget push "src/Infrastructure/SendGridEmailService/bin/Release/EXCSLA.Shared.Infrastructure.SendGridEmailService.5.3.0.nupkg" --api-key $NUGET_API_KEY --source https://api.nuget.org/v3/index.json
```

**Expected output for each:**
```
Pushing EXCSLA.Shared.Core.XXX.X.X.X.nupkg to 'https://api.nuget.org/v3/index.json'...
  PUT https://www.nuget.org/api/v2/package/
  Created https://www.nuget.org/packages/EXCSLA.Shared.Core.XXX/X.X.X
  Your package was pushed.
```

---

## Step 8: Verify on NuGet.org

Wait 2-3 minutes for indexing, then verify packages are live:

- https://www.nuget.org/packages/EXCSLA.Shared.Core.Abstractions/5.2.1
- https://www.nuget.org/packages/EXCSLA.Shared.Core/5.2.1
- https://www.nuget.org/packages/EXCSLA.Shared.Core.ValueObjects/5.2.1
- https://www.nuget.org/packages/EXCSLA.Shared.Core.Specifications/5.2.1
- https://www.nuget.org/packages/EXCSLA.Shared.Core.Exceptions/5.2.1
- https://www.nuget.org/packages/EXCSLA.Shared.Core.GuardClauses/5.2.1
- https://www.nuget.org/packages/EXCSLA.Shared.Core.Abstractions.DomainEventDispatcher/5.2.1
- https://www.nuget.org/packages/EXCSLA.Shared.Infrastructure.SendGridEmailService/5.3.0

---

## Step 9: Commit and Tag

```bash
git add -A

git commit -m "chore: Bump EXCSLA.Shared packages to v5.2.1 (.NET 10 support)

- Updated 7 core packages: 5.2.0 → 5.2.1
- SendGridEmailService: v5.3.0 (unchanged)
- All packages target net10.0
- Updated descriptions with .NET 10 notation
- Published to NuGet.org

Packages published:
- EXCSLA.Shared.Core.Abstractions v5.2.1
- EXCSLA.Shared.Core v5.2.1
- EXCSLA.Shared.Core.ValueObjects v5.2.1
- EXCSLA.Shared.Core.Specifications v5.2.1
- EXCSLA.Shared.Core.Exceptions v5.2.1
- EXCSLA.Shared.Core.GuardClauses v5.2.1
- EXCSLA.Shared.Core.Abstractions.DomainEventDispatcher v5.2.1
- EXCSLA.Shared.Infrastructure.SendGridEmailService v5.3.0"

git tag shared/core/5.2.1
git tag shared/sendgrid/5.3.0

git push origin main
git push origin --tags
```

---

## Step 10: Return to EXCSLA Project

Once all packages are verified on NuGet.org:

1. Share commit hash from EXCSLA.Shared repository
2. Return to EXCSLA main repository
3. Convert ProjectReferences to PackageReferences (v5.2.1)
4. Delete src/Core/EXCSLA.Shared/ directory
5. Update EXCSLA.sln and EXCSLA.WebAPI.sln

---

## Troubleshooting

### Build fails with dependency errors
```bash
rm -rf ~/.nuget/packages
dotnet restore --no-cache
dotnet build
```

### Packages already exist on NuGet
Contact NuGet.org support to unlist, then retry with updated version (e.g., 5.2.2).

### API key invalid
Generate new API key from https://www.nuget.org/account/apikeys with "Push new packages" permission.

### Wrong PackageId in .csproj
Verify PackageId matches expected format:
- Abstractions → `EXCSLA.Shared.Core.Abstractions`
- DomainModels → `EXCSLA.Shared.Core`
- ValueObjects → `EXCSLA.Shared.Core.ValueObjects`
- Specifications → `EXCSLA.Shared.Core.Specifications`
- Exceptions → `EXCSLA.Shared.Core.Exceptions`
- GuardClauses → `EXCSLA.Shared.Core.GuardClauses`
- Abstractions.DomainEventDispatcher → `EXCSLA.Shared.Core.Abstractions.DomainEventDispatcher`
- SendGridEmailService → `EXCSLA.Shared.Infrastructure.SendGridEmailService`

---

## Completion Checklist

- [ ] All 7 core packages v5.2.1 published to NuGet.org
- [ ] SendGridEmailService v5.3.0 confirmed published
- [ ] All packages visible on NuGet.org (wait 2-3 min for indexing)
- [ ] Changes committed to EXCSLA.Shared main branch
- [ ] Tags created: shared/core/5.2.1 and shared/sendgrid/5.3.0
- [ ] Tags pushed to GitHub
- [ ] `dotnet package search EXCSLA.Shared` returns expected versions
- [ ] Ready to convert EXCSLA ProjectReferences to PackageReferences

---

## Package Summary

| Package | Old Version | New Version | PackageId |
|---------|-------------|-------------|-----------|
| Abstractions | 5.2.0 | **5.2.1** | EXCSLA.Shared.Core.Abstractions |
| DomainModels | 5.2.0 | **5.2.1** | EXCSLA.Shared.Core |
| ValueObjects | 5.2.0 | **5.2.1** | EXCSLA.Shared.Core.ValueObjects |
| Specifications | 5.2.0 | **5.2.1** | EXCSLA.Shared.Core.Specifications |
| Exceptions | 5.2.0 | **5.2.1** | EXCSLA.Shared.Core.Exceptions |
| GuardClauses | 5.2.0 | **5.2.1** | EXCSLA.Shared.Core.GuardClauses |
| DomainEventDispatcher | 5.2.0 | **5.2.1** | EXCSLA.Shared.Core.Abstractions.DomainEventDispatcher |
| SendGridEmailService | 5.3.0 | **5.3.0** | EXCSLA.Shared.Infrastructure.SendGridEmailService |

---

## Next Phase

After successful completion, proceed to **EXCSLA Main Repository Phase 3**:
- Convert all ProjectReferences to PackageReferences (v5.2.1)
- Update all .csproj files
- Remove EXCSLA.Shared project entries from solutions
- Delete src/Core/EXCSLA.Shared/ directory
- Build and test all projects
- Commit changes
