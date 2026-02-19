# EXCSLA.Shared Modularization Migration Plan

## Overview
This migration plan outlines the phased approach to restructure the EXCSLA.Shared repository into modular projects aligned with the recommended NuGet package structure. Each phase targets a specific architectural layer, with actionable items for each project/package.

---

## Phase 1: Core Domain Layer

### Projects & Actions
- **EXCSLA.Shared.Core**
  - Move base classes for Aggregates, Entities, Value Objects, and Domain Events into a new `src/Core/EXCSLA.Shared.Core/` project.
  - Create a dedicated `.csproj` file.
- **EXCSLA.Shared.Core.Abstractions**
  - Extract core interfaces and contracts (repositories, specifications, services) into `src/Core/EXCSLA.Shared.Core.Abstractions/`.
  - Create a new `.csproj`.
- **EXCSLA.Shared.Core.Exceptions**
  - Move all custom domain exceptions to `src/Core/EXCSLA.Shared.Core.Exceptions/`.
  - Create a new `.csproj`.
- **EXCSLA.Shared.Core.GuardClauses**
  - Move guard clause extensions to `src/Core/EXCSLA.Shared.Core.GuardClauses/`.
  - Create a new `.csproj`.
- **EXCSLA.Shared.Core.Specifications**
  - Move specification pattern implementation to `src/Core/EXCSLA.Shared.Core.Specifications/`.
  - Create a new `.csproj`.
- **EXCSLA.Shared.Core.ValueObjects.Common**
  - Move common value objects (Email, Phone, Address, etc.) to `src/Core/EXCSLA.Shared.Core.ValueObjects.Common/`.
  - Create a new `.csproj`.
- **EXCSLA.Shared.Core.Abstractions.DomainEventDispatcher**
  - Move domain event dispatcher interfaces to `src/Core/EXCSLA.Shared.Core.Abstractions.DomainEventDispatcher/`.
  - Create a new `.csproj`.

---

## Phase 2: Application Layer

### Projects & Actions
- **EXCSLA.Shared.Application**
  - Move command/query/handler patterns, dispatcher, and application service abstractions to `src/Application/EXCSLA.Shared.Application/`.
  - Create a new `.csproj`.
  - Reference only abstractions from Core.

---

## Phase 3: Infrastructure Layer

### Projects & Actions
- **EXCSLA.Shared.Infrastructure.AlertService**
  - Move alert service implementation to `src/Infrastructure/EXCSLA.Shared.Infrastructure.AlertService/`.
  - Create a new `.csproj`.
- **EXCSLA.Shared.Infrastructure.AzureBlobService**
  - Move Azure Blob Storage integration to `src/Infrastructure/EXCSLA.Shared.Infrastructure.AzureBlobService/`.
  - Create a new `.csproj`.
- **EXCSLA.Shared.Infrastructure.AzureBlobService.Abstractions**
  - Move Azure Blob abstractions to `src/Infrastructure/EXCSLA.Shared.Infrastructure.AzureBlobService.Abstractions/`.
  - Create a new `.csproj`.
- **EXCSLA.Shared.Infrastructure.SendGridEmailService**
  - Move SendGrid email integration to `src/Infrastructure/EXCSLA.Shared.Infrastructure.SendGridEmailService/`.
  - Create a new `.csproj`.
- **EXCSLA.Shared.Infrastructure.DomainEventDispatcher**
  - Move domain event dispatcher implementation to `src/Infrastructure/EXCSLA.Shared.Infrastructure.DomainEventDispatcher/`.
  - Create a new `.csproj`.

---

## Phase 4: Test Projects

### Projects & Actions
- **EXCSLA.Shared.Core.Tests**
  - Move or create tests for Core to `tests/Core/EXCSLA.Shared.Core.Tests/`.
  - Reference only the relevant Core projects.
- **EXCSLA.Shared.Core.ValueObjects.Common.Tests**
  - Move or create tests for ValueObjects.Common to `tests/Core/EXCSLA.Shared.Core.ValueObjects.Common.Tests/`.
- **EXCSLA.Shared.Application.Tests**
  - Move or create tests for Application to `tests/Application/EXCSLA.Shared.Application.Tests/`.
- **EXCSLA.Shared.Infrastructure.*.Tests**
  - Move or create tests for each infrastructure project to `tests/Infrastructure/EXCSLA.Shared.Infrastructure.*.Tests/`.

---

## General Actions (All Phases)
- Update `EXCSLA.Shared.sln` to include all new projects.
- Ensure project references flow inward (Infrastructure → Application → Core).
- Remove or refactor any circular dependencies.
- Update build and CI scripts to pack each project as a separate NuGet package.
- Validate that all tests pass after each phase.

---

**End of Migration Plan**
