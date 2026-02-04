# EXCSLA.Shared Framework Development Plan

## Overview

This document outlines the restructured development plan for the EXCSLA.Shared DDD Framework, focusing on delivering a Minimum Viable Product (MVP) as quickly as possible.

## Project Vision

Build a modern, lightweight DDD framework for .NET that provides:
- Clean separation of concerns following Clean Architecture
- Reusable base classes and patterns for Domain and Application layers
- Simple, dependency-light implementations
- Easy-to-use NuGet packages for rapid application development

## Core Principles

1. **MVP First**: Release Domain and Application layers first, then infrastructure
2. **Simplicity**: Avoid unnecessary external dependencies (no MediatR required)
3. **Modern**: Use latest .NET and C# features
4. **Reusable**: Every package should be independently useful
5. **Well-Documented**: Clear examples and migration guides

## Phase 1: MVP - Core & Application Layers ✅ COMPLETED

**Goal**: Get essential DDD building blocks into developers' hands ASAP.

### Core Domain Layer ✅
**Status**: COMPLETE - Version 5.0.0

Packages released:
- ✅ EXCSLA.Shared.Core (DomainModels)
- ✅ EXCSLA.Shared.Core.Abstractions
- ✅ EXCSLA.Shared.Core.Specifications
- ✅ EXCSLA.Shared.Core.Exceptions
- ✅ EXCSLA.Shared.Core.GuardClauses
- ✅ EXCSLA.Shared.Core.ValueObjects.Common
- ✅ EXCSLA.Shared.Core.Abstractions.AlertService
- ✅ EXCSLA.Shared.Core.Abstractions.DomainEventDispatcher

Key Features:
- Upgraded to .NET 10
- Nullable reference types enabled
- Modern C# with implicit usings
- All projects use project references for better maintainability
- Updated to Ardalis.GuardClauses 5.0.0

### Application Layer ✅
**Status**: COMPLETE - Version 5.0.0

Package released:
- ✅ EXCSLA.Shared.Application

Key Features:
- Command/Query separation (CQRS pattern)
- Simple internal dispatcher using IServiceProvider
- No external dependencies beyond Microsoft.Extensions.DependencyInjection
- Clean interfaces for handlers
- Easy dependency injection setup

### Documentation ✅
**Status**: COMPLETE

Created:
- ✅ README_MVP.md - Comprehensive getting started guide
- ✅ MIGRATION.md - Migration guide from v4.x to v5.0
- ✅ This development plan document

## Phase 2: Infrastructure Layer (Next Priority) 🚧

**Goal**: Provide concrete implementations of domain abstractions.

### Planned Infrastructure Projects

#### 2.1 Core Infrastructure
- [ ] **EntityFrameworkCore** - Base repository implementations
  - Upgrade to .NET 10
  - Update to EF Core 10.x
  - Implement IAsyncRepository<T>
  - Support for Specifications pattern
  
- [ ] **DomainEventDispatcher** - Event dispatching implementation
  - Upgrade to .NET 10
  - Replace Autofac with Microsoft.Extensions.DependencyInjection
  - Support for async event handlers
  - Integration with Application layer dispatcher

#### 2.2 Cloud Services Infrastructure (Lower Priority)
- [ ] **AzureBlobService** - Azure storage integration
  - Upgrade to .NET 10
  - Update Azure.Storage.Blobs to latest
  
- [ ] **SendGridEmailService** - Email service implementation
  - Upgrade to .NET 10
  - Update SendGrid SDK to latest

- [ ] **AlertService** - Alert/notification service
  - Upgrade to .NET 10
  - Consider modernization of implementation

#### 2.3 Identity & Authorization (Lower Priority)
- [ ] **EntityFrameworkCore.Identity** - Identity integration
  - Upgrade to .NET 10
  - Update to latest ASP.NET Core Identity
  
- [ ] **EntityFrameworkCore.ApiAuthorization** - API authorization
  - Upgrade to .NET 10
  - Review and modernize approach

### Infrastructure Timeline

**Week 1-2**: Core Infrastructure
- EntityFrameworkCore base repository
- DomainEventDispatcher refactoring

**Week 3**: Cloud Services
- Azure Blob Service
- SendGrid Email Service

**Week 4**: Identity & Authorization
- EF Core Identity integration
- API Authorization

## Phase 3: UI Components (Future) 📅

**Goal**: Provide reusable Blazor components.

### Planned UI Projects
- [ ] Blazor.Client.AlertService - Client-side alerts
- [ ] Blazor.Client.HttpApiClient - HTTP client helpers
- [ ] Blazor.Client.ServerSideValidator - Server validation
- [ ] Blazor.DataTable - Data table component
- [ ] Blazor.LoadingSpinner - Loading indicators
- [ ] Blazor.Markdown - Markdown rendering
- [ ] Blazor.Modal - Modal dialogs

All UI projects will:
- Upgrade to .NET 10
- Use latest Blazor features
- Be independently usable
- Include examples and documentation

## Phase 4: Testing & Quality 📅

**Goal**: Ensure framework reliability and quality.

### Test Coverage
- [ ] Expand unit test coverage for Core
- [ ] Add tests for Application layer
- [ ] Integration tests for Infrastructure
- [ ] UI component tests
- [ ] Performance benchmarks

### Quality Improvements
- [ ] Address nullable reference type warnings
- [ ] Remove obsolete serialization constructors
- [ ] Code analysis and cleanup
- [ ] Documentation reviews

## Release Strategy

### MVP Release (v5.0.0) ✅ READY
**Packages**:
- Core domain packages (8 packages)
- Application layer package (1 package)

**Target Date**: Immediate - ready for release

**Release Notes**:
- Complete rewrite for .NET 10
- New Application layer with CQRS support
- No breaking changes to domain model APIs
- Migration guide provided

### Infrastructure Release (v5.1.0) 🚧 NEXT
**Packages**:
- EntityFrameworkCore packages
- DomainEventDispatcher
- Cloud service integrations

**Target Date**: 2-3 weeks after MVP

**Release Notes**:
- Infrastructure layer upgraded to .NET 10
- Removed Autofac dependency
- Updated to latest EF Core and Azure SDKs

### UI Release (v5.2.0) 📅 FUTURE
**Packages**:
- All Blazor components

**Target Date**: 4-6 weeks after MVP

**Release Notes**:
- Blazor components upgraded to .NET 10
- Modern Blazor features
- Enhanced styling and customization

## Development Workflow

### For MVP Maintenance
1. Bug fixes go directly to main branch
2. Small improvements via PR
3. Documentation updates as needed

### For New Features
1. Create feature branch
2. Implement with tests
3. Update documentation
4. Code review
5. Merge to main
6. Version bump and release

## Success Metrics

### MVP Success Criteria ✅
- [x] All Core packages build and pack successfully
- [x] Application layer package builds and packs successfully
- [x] Comprehensive documentation provided
- [x] Migration guide available
- [x] All packages versioned as 5.0.0

### Infrastructure Success Criteria 🚧
- [ ] EF Core repository working with .NET 10
- [ ] Domain events dispatching correctly
- [ ] All infrastructure tests passing
- [ ] Azure and SendGrid integrations working

### Overall Success Criteria 📅
- [ ] Framework used in at least 3 production projects
- [ ] 80%+ code coverage
- [ ] Positive community feedback
- [ ] Active maintenance and updates

## Technical Debt & Known Issues

### Current Technical Debt
1. **Serialization Warnings**: Custom exceptions use obsolete serialization constructors
   - Impact: Low (just warnings)
   - Priority: Low
   - Plan: Address in v5.1.0

2. **Nullable Warnings in Domain Models**: Some nullable reference type warnings
   - Impact: Low (just warnings)
   - Priority: Medium
   - Plan: Address incrementally

### Future Improvements
1. **Source Generators**: Consider using source generators for boilerplate
2. **Analyzers**: Create Roslyn analyzers for DDD patterns
3. **Templates**: Provide dotnet templates for common scenarios
4. **Examples**: Create example applications

## Dependencies Management

### Current Dependencies
- **Ardalis.GuardClauses**: v5.0.0 (Core.DomainModels)
- **Microsoft.Extensions.DependencyInjection.Abstractions**: v9.0.0 (Application)

### Dependency Strategy
- Keep dependencies minimal
- Use only stable, well-maintained packages
- Prefer Microsoft packages when possible
- Avoid dependencies that go commercial (learned from MediatR situation)

## Community & Support

### Documentation
- [x] README with quick start
- [x] Migration guide
- [x] API documentation (XML comments)
- [ ] Wiki with detailed guides
- [ ] Example projects
- [ ] Video tutorials

### Support Channels
- GitHub Issues for bugs
- GitHub Discussions for questions
- Pull requests for contributions
- Regular maintenance and updates

## Conclusion

This plan focuses on delivering value quickly while maintaining quality. The MVP (Phase 1) is complete and ready for release. Infrastructure (Phase 2) will follow within 2-3 weeks, and UI components (Phase 3) will come later as needed.

The framework is designed to be:
- **Practical**: Solve real problems
- **Simple**: Easy to understand and use
- **Modern**: Use latest .NET features
- **Maintainable**: Clear structure and dependencies

---

**Last Updated**: February 2026  
**Current Phase**: Phase 1 Complete ✅, Phase 2 Planning 🚧  
**Next Milestone**: Infrastructure Release v5.1.0
