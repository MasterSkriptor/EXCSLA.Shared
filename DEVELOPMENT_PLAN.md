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

#### 2.1 Core Infrastructure (REMOVED - Using Application Layer)

The following projects have been **REMOVED** as the Application layer now handles event dispatching:
- ~~DomainEventDispatcher~~ - Removed (replaced by Application layer Dispatcher)
- ~~EntityFrameworkCore~~ - Removed (will be rebuilt in v5.1.0 if needed)
- ~~EntityFrameworkCore.Standard~~ - Removed
- ~~EntityFrameworkCore.Identity~~ - Removed
- ~~EntityFrameworkCore.ApiAuthorization~~ - Removed

**Note**: The Application layer's `IDispatcher` now handles command/query dispatching. Domain events can be dispatched through the same mechanism or by implementing custom handlers.

#### 2.2 Cloud Services Infrastructure (Lower Priority)
- [ ] **AzureBlobService** - Azure storage integration
  - Upgrade to .NET 10
  - Update Azure.Storage.Blobs to latest
  
- [ ] **SendGridEmailService** - Email service implementation
  - Upgrade to .NET 10
  - Update SendGrid SDK to latest

#### 2.3 Identity & Authorization (REMOVED)
The following projects have been **REMOVED**:
- ~~EntityFrameworkCore.Identity~~ - Removed
- ~~EntityFrameworkCore.ApiAuthorization~~ - Removed

**Note**: These will be rebuilt from scratch in a future release if needed, following modern .NET identity patterns.

### Infrastructure Timeline

**Phase 2 has been simplified**: Since EF Core and event dispatching infrastructure were removed, the focus is now on:

**Week 1-2**: Cloud Services Only
- Azure Blob Service
- SendGrid Email Service
- Alert Service (if needed)

~~**Week 3-4**: Identity & Authorization~~ - **REMOVED**

## Phase 3: UI Components (Future - v5.2.0) 📅

**Goal**: Provide reusable Blazor components.

**Status**: Deferred - Components removed from this release

**Reason for Deferral**: 
- Focus MVP on Domain and Application layers first
- Allow time to resolve Blazor/BlazorStrap .NET 10 integration issues
- Maintain clean build with zero errors

### What Was Attempted (Phase 3 Work)

During development, an attempt was made to migrate the following UI projects to .NET 10:
- Blazor.Client.AlertService
- Blazor.Client.HttpApiClient
- Blazor.Client.ServerSideValidator
- Blazor.DataTable
- Blazor.LoadingSpinner
- Blazor.Markdown
- Blazor.Modal

**Issues Encountered**:
1. BlazorStrap component namespace resolution in Razor SDK
2. ProjectReference vs PackageReference dependency resolution in Blazor projects
3. ImplicitUsings interaction with Razor file-scoped namespaces
4. RootNamespace configuration conflicts

### Phase 3 Timeline (v5.2.0 Release)

**Expected**: 4-6 weeks after MVP release

**What Will Be Included**:
- All 7 Blazor components upgraded to .NET 10
- Proper BlazorStrap 5.2.104 integration
- Modern Blazor 10 features
- Comprehensive examples and documentation
- Full test coverage

---

## Phase 4: Testing & Quality � IN PROGRESS

**Goal**: Ensure framework reliability and quality.

**Status**: Current phase - Building comprehensive test suite and improving code quality

### Test Coverage
- [ ] Expand unit test coverage for Core
- [ ] Add tests for Application layer
- [ ] Integration tests for Infrastructure
- [ ] UI component tests (after Phase 3 completion)
- [ ] Performance benchmarks

### Quality Improvements
- [ ] Address nullable reference type warnings
- [ ] Remove obsolete serialization constructors
- [ ] Code analysis and cleanup
- [ ] Documentation reviews
- [ ] Verify all projects follow consistent patterns

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

### Infrastructure Release (v5.1.0) ⏳ PLANNED
**Packages**:
- Cloud service integrations (AzureBlobService, SendGridEmailService)
- EntityFrameworkCore packages (if needed)
- DomainEventDispatcher (if needed)

**Target Date**: After Phase 4 testing (3-4 weeks)

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
**Current Phase**: Phase 1 Complete ✅, Phase 2 Complete ✅, Phase 3 Deferred, Phase 4 In Progress 🚧  
**Next Milestone**: v5.0.0 MVP Release + Phase 4 Quality Improvements
