# EXCSLA.Shared DDD Framework - Product Requirements Document (PRD)

**Version:** 1.0  
**Date:** February 2026  
**Author:** Executive Computer Systems, LLC  
**Status:** Planning Phase

---

## Executive Summary

This PRD outlines the comprehensive modernization plan for the EXCSLA.Shared Domain-Driven Design (DDD) Framework. The project will upgrade the framework from .NET 6.0 to .NET 10, implement modern C# language features, enhance DDD and Clean Architecture patterns, audit and update all dependencies, and establish comprehensive test coverage. This effort is designed to be executed by a single developer leveraging GitHub Copilot and modern development tools to achieve rapid, cost-effective delivery.

### Key Objectives
1. Migrate entire codebase to .NET 10
2. Modernize C# code using latest language features
3. Refactor architecture to align with modern DDD and Clean Architecture principles
4. Audit, update, and remove outdated dependencies
5. Achieve maximum code coverage through comprehensive testing
6. Deploy updated NuGet packages efficiently with zero infrastructure cost

---

## 1. Current State Analysis

### 1.1 Repository Overview
- **Repository:** MasterSkriptor/EXCSLA.Shared
- **Purpose:** Domain-Driven Design core framework and shared components for EXCSLA websites and APIs
- **Current Target Framework:** .NET 6.0 / .NET Standard 2.1
- **License:** LGPL-3.0-or-later
- **Current Version:** 4.0.1

### 1.2 Project Structure
The repository contains 26 projects organized into three main categories:

#### Core Projects (10 projects)
- **EXCSLA.Shared.Core (DomainModels)** - Base classes for Aggregates, Entities, Value Objects
- **EXCSLA.Shared.Core.Abstractions** - Core interfaces and contracts
- **EXCSLA.Shared.Core.Abstractions.AlertService** - Alert service interfaces
- **EXCSLA.Shared.Core.Abstractions.DomainEventDispatcher** - Domain event interfaces
- **EXCSLA.Shared.Core.Exceptions** - Custom domain exceptions
- **EXCSLA.Shared.Core.GuardClauses** - Input validation extensions
- **EXCSLA.Shared.Core.Specifications** - Specification pattern implementation
- **EXCSLA.Shared.Core.ValueObjects.Common** - Common value objects (Email, Phone, Address, etc.)

#### Infrastructure Projects (8 projects)
- **EXCSLA.Shared.Infrastructure.AlertService** - Bootstrap 4 alert service implementation
- **EXCSLA.Shared.Infrastructure.AzureBlobService** - Azure Blob Storage integration
- **EXCSLA.Shared.Infrastructure.AzureBlobService.Abstractions** - Azure Blob abstractions
- **EXCSLA.Shared.Infrastructure.SendGridEmailService** - SendGrid email integration
- **EXCSLA.Shared.Infrastructure.EntityFrameworkCore** - EF Core base repository
- **EXCSLA.Shared.Infrastructure.EntityFrameworkCore.Standard** - EF Core standard implementation
- **EXCSLA.Shared.Infrastructure.EntityFrameworkCore.Identity** - Identity integration
- **EXCSLA.Shared.Infrastructure.EntityFrameworkCore.ApiAuthorization** - API authorization
- **EXCSLA.Shared.Infrastructure.DomainEventDispatcher** - Domain event dispatcher

#### UI Projects (to be identified)

#### Test Projects (2 projects)
- **EXCSLA.Shared.Core.Tests**
- **EXCSLA.Shared.Core.ValueObjects.Common.Tests**

### 1.3 Current Technical Debt
1. **Outdated Framework:** .NET 6.0 (end of support November 2024)
2. **Legacy C# Syntax:** Traditional namespaces, verbose property declarations
3. **Inconsistent Target Frameworks:** Mix of net6.0 and netstandard2.1
4. **Outdated Dependencies:**
   - Microsoft.EntityFrameworkCore 6.0.0 (latest: 10.x)
   - Azure.Storage.Blobs 12.10.0 (latest: 12.x+)
   - Sendgrid 9.25.0 (latest: 9.x+)
   - Ardalis.GuardClauses 3.3.0 (latest: 4.x+)
   - xUnit test packages from 2020
5. **Limited Test Coverage:** Only 2 test projects for 24+ production projects
6. **Build Pipeline:** Uses outdated Azure DevOps configuration with NuGet 4.9.3

---

## 2. .NET 10 Migration Plan

### 2.1 Pre-Migration Preparation

#### 2.1.1 Environment Setup
- [ ] Install .NET 10 SDK
- [ ] Update Visual Studio / VS Code to support .NET 10
- [ ] Install/update necessary global tools (dotnet-format, dotnet-outdated, etc.)
- [ ] Configure GitHub Copilot for optimal code suggestions

#### 2.1.2 Compatibility Analysis
- [ ] Run .NET Upgrade Assistant analysis on all projects
- [ ] Document API breaking changes between .NET 6 and .NET 10
- [ ] Identify obsolete APIs and their replacements
- [ ] Verify Azure SDK compatibility with .NET 10
- [ ] Verify SendGrid SDK compatibility with .NET 10

### 2.2 Migration Execution Strategy

**Phased Approach (Bottom-Up):**

#### Phase 1: Core Projects (Week 1-2)
1. Update all Core projects to `<TargetFramework>net10.0</TargetFramework>`
2. Update SDK references: `<Project Sdk="Microsoft.NET.Sdk">`
3. Build and fix compilation errors
4. Run existing tests to verify no regression
5. Update project version to 5.0.0 (major version bump)

#### Phase 2: Infrastructure Projects (Week 2-3)
1. Update Infrastructure projects targeting Core projects
2. Address any EF Core breaking changes
3. Update Azure Blob Storage service implementation
4. Update SendGrid service implementation
5. Verify all Infrastructure tests pass

#### Phase 3: UI Projects (Week 3-4)
1. Identify and update UI-related projects
2. Update Razor components if any
3. Verify UI integration tests

#### Phase 4: Test Projects (Week 4)
1. Update all test projects to net10.0
2. Update test runner and framework packages
3. Ensure all tests compile and pass

### 2.3 Breaking Changes Mitigation
- Document all API changes in CHANGELOG.md
- Provide migration guide for consumers
- Consider marking old APIs with `[Obsolete]` before removal
- Create upgrade scripts if necessary

---

## 3. Modern C# Features Migration

### 3.1 File-Scoped Namespaces

**Current State:**
```csharp
namespace EXCSLA.Shared.Infrastructure.Services
{
    public class AlertService : IAlertService
    {
        // implementation
    }
}
```

**Target State:**
```csharp
namespace EXCSLA.Shared.Infrastructure.Services;

public class AlertService : IAlertService
{
    // implementation
}
```

**Migration Plan:**
- [ ] Enable .editorconfig rule for file-scoped namespaces
- [ ] Use bulk refactoring tool or Roslyn analyzer
- [ ] Process all ~100+ .cs files systematically
- [ ] Verify no regression after conversion

### 3.2 Global Using Directives

**Implementation:**
- [ ] Create GlobalUsings.cs in each project
- [ ] Move common using statements (System, System.Linq, System.Collections.Generic)
- [ ] Enable implicit global usings where appropriate
- [ ] Reduce code clutter across files

**Example GlobalUsings.cs:**
```csharp
global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Threading;
global using System.Threading.Tasks;
```

### 3.3 Init-Only Properties

**Refactor immutable value objects:**
```csharp
// Current
public string Value { get; }
public EmailAddress(string email) { Value = email; }

// Target
public string Value { get; init; }
```

### 3.4 Record Types

**Value Objects Conversion:**
- [ ] Convert appropriate value objects to record types
- [ ] Leverage built-in equality semantics
- [ ] Use primary constructors for conciseness

**Example:**
```csharp
public record EmailAddress(string Value)
{
    // Validation in constructor or init
}
```

### 3.5 Pattern Matching Enhancements

- [ ] Use switch expressions with pattern matching
- [ ] Apply property patterns for complex conditions
- [ ] Use relational patterns where applicable

### 3.6 Nullable Reference Types

- [ ] Enable `<Nullable>enable</Nullable>` across all projects
- [ ] Annotate all public APIs with nullability
- [ ] Fix null-reference warnings
- [ ] Use nullable context for stricter null safety

### 3.7 Required Members (C# 11+)

**For DTOs and configuration classes:**
```csharp
public class SendGridOptions
{
    public required string Key { get; init; }
    public required string SendFromEmailAddress { get; init; }
    public required string ReplyToEmailAddress { get; init; }
}
```

### 3.8 Raw String Literals

- [ ] Use for multi-line strings, JSON templates, SQL queries
- [ ] Reduce escape character noise

### 3.9 List Patterns (C# 11+)

- [ ] Apply in validation logic and collection processing
- [ ] Simplify sequence matching code

### 3.10 Primary Constructors (C# 12+)

**For simple classes:**
```csharp
public class AlertService(ILogger<AlertService> logger) : IAlertService
{
    private readonly List<Alert> _alerts = new();
}
```

---

## 4. DDD and Clean Architecture Modernization

### 4.1 Current Architecture Assessment

**Strengths:**
- Clear separation between Core, Infrastructure, and UI
- Use of Aggregates, Entities, and Value Objects
- Repository and Specification patterns implemented
- Domain events infrastructure in place

**Areas for Improvement:**
- Inconsistent use of DDD tactical patterns
- Some business logic may leak into infrastructure
- Limited use of Domain Services
- No explicit Application layer
- Command/Query separation not enforced

### 4.2 Clean Architecture Enhancements

#### 4.2.1 Introduce Application Layer
- [ ] Create EXCSLA.Shared.Application project(s)
- [ ] Implement CQRS pattern using MediatR or similar
- [ ] Define Commands, Queries, and their Handlers
- [ ] Separate read models from write models

**Structure:**
```
src/Application/
├── Commands/
│   ├── CreateEntity/
│   │   ├── CreateEntityCommand.cs
│   │   └── CreateEntityCommandHandler.cs
├── Queries/
│   ├── GetEntity/
│   │   ├── GetEntityQuery.cs
│   │   └── GetEntityQueryHandler.cs
├── DTOs/
└── Interfaces/
```

#### 4.2.2 Strengthen Domain Layer
- [ ] Review and enhance Aggregate boundaries
- [ ] Ensure invariants are enforced in Aggregates
- [ ] Extract Domain Services for complex business logic
- [ ] Implement Domain Events more comprehensively
- [ ] Add Specification pattern for complex queries

#### 4.2.3 Infrastructure Improvements
- [ ] Ensure Infrastructure only implements interfaces from Core/Application
- [ ] Improve EF Core configurations (separate IEntityTypeConfiguration)
- [ ] Implement Unit of Work pattern more explicitly
- [ ] Add outbox pattern for reliable domain event publishing

#### 4.2.4 Dependency Inversion
- [ ] Verify all dependencies point inward (Infrastructure → Application → Domain)
- [ ] Remove any Core/Domain dependencies on Infrastructure
- [ ] Use Dependency Injection consistently

### 4.3 Tactical DDD Patterns Enhancement

#### 4.3.1 Value Objects
- [ ] Audit all value objects for proper immutability
- [ ] Ensure value objects validate themselves in constructor
- [ ] Add factory methods where appropriate
- [ ] Consider record types for simple value objects

#### 4.3.2 Entities and Aggregates
- [ ] Review aggregate root boundaries
- [ ] Ensure entities inside aggregates aren't directly accessible
- [ ] Implement proper encapsulation (private setters)
- [ ] Add factory methods for complex creation logic

#### 4.3.3 Domain Events
- [ ] Standardize domain event naming (past tense: OrderPlacedEvent)
- [ ] Ensure events are immutable
- [ ] Add timestamp and correlation ID to all events
- [ ] Implement event versioning strategy

#### 4.3.4 Repositories
- [ ] Ensure repositories work only with aggregate roots
- [ ] Remove any "UpdateEntity" methods (use aggregate methods)
- [ ] Consider async-only APIs
- [ ] Add specification-based query methods

#### 4.3.5 Domain Services
- [ ] Extract multi-aggregate operations to Domain Services
- [ ] Keep services focused and cohesive
- [ ] Use meaningful service names (OrderPricingService, not OrderService)

### 4.4 Code Organization Standards

- [ ] Establish consistent folder structure across projects
- [ ] Create architectural decision records (ADRs)
- [ ] Document ubiquitous language in glossary
- [ ] Add code examples and best practices to docs

---

## 5. Dependency Audit and Update Strategy

### 5.1 Dependency Inventory

#### 5.1.1 Microsoft Packages
| Package | Current | Latest | Action |
|---------|---------|--------|--------|
| Microsoft.EntityFrameworkCore | 6.0.0 | 10.0.x | Update |
| Microsoft.EntityFrameworkCore.SqlServer | 6.0.0 | 10.0.x | Update |
| Microsoft.NET.Test.Sdk | 16.4.0 | 17.x+ | Update |

#### 5.1.2 Third-Party Packages
| Package | Current | Latest | Action | Notes |
|---------|---------|--------|--------|-------|
| Ardalis.GuardClauses | 3.3.0 | 5.x | Update | Breaking changes possible |
| Azure.Storage.Blobs | 12.10.0 | 12.x | Update | Verify API compatibility |
| Sendgrid | 9.25.0 | 9.x | Update | Check for deprecations |
| xunit | 2.4.1 | 2.9.x | Update | Test framework |
| xunit.runner.visualstudio | 2.4.1 | 2.8.x | Update | Test runner |

### 5.2 Dependency Update Process

#### 5.2.1 Automated Dependency Analysis
```bash
# Install dotnet-outdated tool
dotnet tool install -g dotnet-outdated-tool

# Run analysis
dotnet outdated --upgrade

# Review security vulnerabilities
dotnet list package --vulnerable --include-transitive
```

#### 5.2.2 Update Methodology
1. **Update one package at a time** (for critical packages)
2. **Run full test suite** after each update
3. **Document breaking changes** in migration notes
4. **Test NuGet package generation** after updates
5. **Verify no unexpected transitive dependencies** added

#### 5.2.3 Package Version Management
- [ ] Centralize package versions using Directory.Packages.props
- [ ] Enable Central Package Management (CPM) in .NET 10
- [ ] Define version constraints (e.g., [6.0.0,7.0.0))

**Directory.Packages.props Example:**
```xml
<Project>
  <PropertyGroup>
    <ManagePackageVersionsCentrally>true</ManagePackageVersionsCentrally>
  </PropertyGroup>
  <ItemGroup>
    <PackageVersion Include="Microsoft.EntityFrameworkCore" Version="10.0.0" />
    <PackageVersion Include="Ardalis.GuardClauses" Version="5.0.0" />
  </ItemGroup>
</Project>
```

### 5.3 Dependency Removal Strategy

#### 5.3.1 Identification Criteria
- Packages not compatible with .NET 10
- Packages with no active maintenance (>2 years no updates)
- Packages with critical security vulnerabilities
- Redundant packages (overlapping functionality)

#### 5.3.2 Removal Process
1. **Identify dependencies** of the package to be removed
2. **Find alternatives** or implement functionality in-house
3. **Create compatibility layer** if needed for consumers
4. **Update documentation** noting the removal
5. **Deprecate** in version N, remove in version N+1

#### 5.3.3 Candidate Packages for Review
- Any netstandard2.0 only packages without .NET 6+ support
- Legacy logging frameworks (if using ILogger now)
- Old JSON serialization libraries (use System.Text.Json)

### 5.4 Security Auditing

- [ ] Enable Dependabot alerts on GitHub
- [ ] Run `dotnet list package --vulnerable` in CI/CD
- [ ] Set up automated dependency updates via Dependabot or Renovate
- [ ] Review and fix all high/critical security vulnerabilities before release

---

## 6. Comprehensive Testing Strategy

### 6.1 Current Test Coverage Analysis

**Current State:**
- 2 test projects covering primarily Core.ValueObjects.Common
- Limited coverage of Infrastructure layer
- No integration tests identified
- No performance tests
- Test framework: xUnit

**Coverage Goals:**
- **Unit Tests:** 90%+ code coverage
- **Integration Tests:** All Infrastructure services
- **End-to-End Tests:** Key scenarios for framework usage
- **Performance Tests:** Repository operations, large collections

### 6.2 Test Project Structure

#### 6.2.1 New Test Projects to Create
```
Tests/
├── Unit/
│   ├── EXCSLA.Shared.Core.Tests
│   ├── EXCSLA.Shared.Core.Abstractions.Tests
│   ├── EXCSLA.Shared.Core.Exceptions.Tests
│   ├── EXCSLA.Shared.Core.GuardClauses.Tests
│   ├── EXCSLA.Shared.Core.Specifications.Tests
│   ├── EXCSLA.Shared.Core.ValueObjects.Common.Tests (exists)
├── Integration/
│   ├── EXCSLA.Shared.Infrastructure.EntityFrameworkCore.Tests
│   ├── EXCSLA.Shared.Infrastructure.AzureBlobService.Tests
│   ├── EXCSLA.Shared.Infrastructure.SendGridEmailService.Tests
│   ├── EXCSLA.Shared.Infrastructure.DomainEventDispatcher.Tests
├── E2E/
│   ├── EXCSLA.Shared.E2E.Tests
└── Performance/
    └── EXCSLA.Shared.Performance.Tests
```

### 6.3 Testing Frameworks and Tools

#### 6.3.1 Core Testing Stack
- **Test Framework:** xUnit (upgrade to latest)
- **Mocking:** Moq or NSubstitute
- **Assertions:** FluentAssertions for readable assertions
- **Test Data:** AutoFixture or Bogus for generating test data
- **Coverage:** Coverlet for code coverage collection
- **Reporting:** ReportGenerator for coverage reports

#### 6.3.2 Integration Testing
- **Database:** Use TestContainers for SQL Server or SQLite in-memory
- **Azure Storage:** Use Azurite (Azure Storage Emulator) via TestContainers
- **Email:** Mock SendGrid client or use local SMTP server

#### 6.3.3 Performance Testing
- **BenchmarkDotNet:** For micro-benchmarks
- **Load Testing:** k6 or NBomber for API load tests (if applicable)

### 6.4 Unit Testing Strategy

#### 6.4.1 Test Naming Convention
```csharp
public class EmailAddress_Should
{
    [Fact]
    public void Create_ValidEmail_ReturnsEmailAddress() { }
    
    [Fact]
    public void Throw_InvalidEmail_ThrowsArgumentException() { }
}
```

#### 6.4.2 AAA Pattern (Arrange-Act-Assert)
```csharp
[Fact]
public void AddAlert_ValidMessage_AddsAlertToList()
{
    // Arrange
    var sut = new AlertService();
    var message = "Test alert";
    
    // Act
    sut.AddAlert(message, AlertType.Info);
    
    // Assert
    sut.Alerts.Should().HaveCount(1);
    sut.Alerts[0].Message.Should().Be(message);
}
```

#### 6.4.3 Test Coverage Requirements
- **Value Objects:** 100% - All validation logic, equality, immutability
- **Entities/Aggregates:** 95%+ - All business logic and invariants
- **Domain Services:** 90%+ - All business rules
- **Specifications:** 100% - Query logic correctness
- **Guards:** 100% - All validation paths

### 6.5 Integration Testing Strategy

#### 6.5.1 EF Core Repository Tests
```csharp
public class EntityFrameworkRepository_Should : IClassFixture<DatabaseFixture>
{
    [Fact]
    public async Task GetByIdAsync_ExistingEntity_ReturnsEntity() 
    {
        // Use real database (TestContainers)
        // Verify CRUD operations
        // Test transaction behavior
    }
}
```

#### 6.5.2 Azure Blob Service Tests
- Use Azurite for local testing
- Test upload, download, delete operations
- Verify error handling

#### 6.5.3 SendGrid Email Service Tests
- Mock external API calls
- Verify email composition
- Test error scenarios

### 6.6 Test Automation and CI/CD Integration

#### 6.6.1 Local Test Execution
```bash
# Run all tests
dotnet test

# Run with coverage
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover

# Generate coverage report
reportgenerator -reports:coverage.opencover.xml -targetdir:coveragereport
```

#### 6.6.2 CI/CD Pipeline Tests
- [ ] Run unit tests on every PR
- [ ] Run integration tests on merge to main
- [ ] Require 90% coverage for PR approval
- [ ] Publish coverage reports to GitHub Actions artifacts
- [ ] Block merge if critical tests fail

#### 6.6.3 Test Data Management
- [ ] Use builders/factories for test object creation
- [ ] Create shared test fixtures for common scenarios
- [ ] Seed databases with representative data
- [ ] Use realistic data (Bogus library)

### 6.7 Performance Testing

#### 6.7.1 Benchmark Tests
```csharp
[MemoryDiagnoser]
public class RepositoryBenchmarks
{
    [Benchmark]
    public async Task<List<Entity>> GetAllEntities()
    {
        // Benchmark repository query performance
    }
}
```

#### 6.7.2 Performance Targets
- Repository queries: < 100ms for simple queries
- Aggregate hydration: < 50ms
- Domain event dispatch: < 10ms per event
- Value object creation: < 1ms

---

## 7. Modernization Implementation Plan

### 7.1 Project Timeline (Single Developer with Copilot)

| Phase | Duration | Effort | Milestones |
|-------|----------|--------|------------|
| **Phase 0: Preparation** | 1 week | 5 days | Environment setup, analysis complete |
| **Phase 1: Core Migration** | 2 weeks | 10 days | All Core projects on .NET 10 with modern C# |
| **Phase 2: Infrastructure Migration** | 2 weeks | 10 days | All Infrastructure projects updated |
| **Phase 3: Testing Foundation** | 2 weeks | 10 days | Test infrastructure, 50% coverage |
| **Phase 4: DDD Refactoring** | 3 weeks | 15 days | Clean Architecture patterns applied |
| **Phase 5: Test Completion** | 2 weeks | 10 days | 90%+ coverage achieved |
| **Phase 6: Documentation & Release** | 1 week | 5 days | Docs, samples, NuGet publish |
| **Total** | **13 weeks** | **65 days** | ~3 months |

### 7.2 Detailed Phase Breakdown

#### Phase 0: Preparation (Week 1)
- [ ] Install .NET 10 SDK and tooling
- [ ] Configure Copilot workspace for optimal suggestions
- [ ] Create project tracking board (GitHub Projects)
- [ ] Document current architecture (diagrams)
- [ ] Run dependency audit with `dotnet outdated`
- [ ] Create git branches: feature/dotnet10-migration
- [ ] Set up local development environment
- [ ] Create baseline performance benchmarks

#### Phase 1: Core Migration (Weeks 2-3)
**Week 2: Framework & Language Updates**
- [ ] Update all Core .csproj files to net10.0
- [ ] Enable nullable reference types
- [ ] Convert to file-scoped namespaces (bulk operation)
- [ ] Add GlobalUsings.cs to each Core project
- [ ] Update NuGet package metadata to version 5.0.0-alpha1
- [ ] Build and resolve compilation errors
- [ ] Run existing Core tests

**Week 3: Modern C# Features**
- [ ] Convert value objects to record types where appropriate
- [ ] Apply init-only properties
- [ ] Use primary constructors for simple classes
- [ ] Apply pattern matching enhancements
- [ ] Add required members to DTOs
- [ ] Fix all nullable warnings
- [ ] Update unit tests, verify all passing

#### Phase 2: Infrastructure Migration (Weeks 4-5)
**Week 4: EF Core & Azure Services**
- [ ] Update EntityFrameworkCore projects to .NET 10
- [ ] Update EF Core packages to 10.x
- [ ] Refactor EF configurations using modern syntax
- [ ] Update Azure.Storage.Blobs package
- [ ] Test Azure Blob Service with Azurite
- [ ] Update DomainEventDispatcher
- [ ] Apply file-scoped namespaces

**Week 5: Email & Other Infrastructure**
- [ ] Update SendGrid package
- [ ] Update AlertService project
- [ ] Update Identity and ApiAuthorization projects
- [ ] Apply modern C# features across Infrastructure
- [ ] Build all Infrastructure projects
- [ ] Run smoke tests

#### Phase 3: Testing Foundation (Weeks 6-7)
**Week 6: Test Infrastructure Setup**
- [ ] Create test project structure (Unit/Integration/E2E/Performance)
- [ ] Update existing test projects to .NET 10
- [ ] Add FluentAssertions, Moq, AutoFixture
- [ ] Configure Coverlet for code coverage
- [ ] Set up TestContainers for integration tests
- [ ] Create base test classes and fixtures
- [ ] Document testing standards

**Week 7: Initial Test Implementation**
- [ ] Write unit tests for all Value Objects (100% coverage)
- [ ] Write unit tests for Entities and Aggregates (50% coverage)
- [ ] Write integration tests for EF Core repositories (basic CRUD)
- [ ] Set up CI pipeline to run tests
- [ ] Generate first coverage report (target: 50%+)

#### Phase 4: DDD Refactoring (Weeks 8-10)
**Week 8: Domain Layer Enhancement**
- [ ] Review and strengthen Aggregate boundaries
- [ ] Extract Domain Services from complex logic
- [ ] Enhance Domain Events (versioning, metadata)
- [ ] Improve Specification implementations
- [ ] Add factory methods to Aggregates
- [ ] Ensure invariants are properly enforced
- [ ] Write tests for new Domain Services

**Week 9: Application Layer Introduction (Optional)**
- [ ] Create Application layer project structure
- [ ] Implement CQRS with simple Command/Query pattern
- [ ] Create DTOs for data transfer
- [ ] Implement simple Handlers
- [ ] Add validation pipeline
- [ ] Document Application layer patterns

**Week 10: Infrastructure Refinement**
- [ ] Separate EF Core entity configurations (IEntityTypeConfiguration)
- [ ] Implement Unit of Work more explicitly
- [ ] Consider Outbox pattern for events (document decision)
- [ ] Ensure proper dependency direction
- [ ] Update Infrastructure tests for new patterns
- [ ] Verify all projects build and tests pass

#### Phase 5: Test Completion (Weeks 11-12)
**Week 11: Unit Test Completion**
- [ ] Write unit tests for all Entities (95%+ coverage)
- [ ] Write unit tests for all Domain Services (90%+ coverage)
- [ ] Write unit tests for Specifications (100% coverage)
- [ ] Write unit tests for GuardClauses (100% coverage)
- [ ] Write unit tests for Infrastructure services (mocked, 80%+ coverage)
- [ ] Achieve 80%+ overall unit test coverage

**Week 12: Integration & Performance Tests**
- [ ] Complete integration tests for all EF Core repositories
- [ ] Integration tests for Azure Blob Service (with Azurite)
- [ ] Integration tests for SendGrid Email Service (mocked)
- [ ] Integration tests for Domain Event Dispatcher
- [ ] Create BenchmarkDotNet performance tests for critical paths
- [ ] Achieve 90%+ overall test coverage
- [ ] Document performance baselines

#### Phase 6: Documentation & Release (Week 13)
- [ ] Update README.md with .NET 10 information
- [ ] Create MIGRATION_GUIDE.md for consumers
- [ ] Write CHANGELOG.md detailing all changes
- [ ] Update XML documentation comments across codebase
- [ ] Create code samples for common scenarios
- [ ] Update Azure DevOps pipeline for .NET 10
- [ ] Test NuGet package generation locally
- [ ] Bump version to 5.0.0-beta1
- [ ] Publish beta packages to NuGet (test)
- [ ] Final testing and verification
- [ ] Release 5.0.0 to NuGet
- [ ] Create GitHub release with notes

---

## 8. Leveraging GitHub Copilot for Efficiency

### 8.1 Copilot Usage Strategies

#### 8.1.1 Code Modernization
- Use Copilot to suggest file-scoped namespace conversions
- Generate global using directives
- Convert properties to init-only setters
- Suggest record type conversions
- Generate nullable annotations

**Example Prompts:**
- "Convert this class to use file-scoped namespaces"
- "Make this value object immutable using init properties"
- "Convert this class to a record type"
- "Add nullable annotations to this method"

#### 8.1.2 Test Generation
- Generate test methods from production code
- Create test data with AutoFixture
- Write assertion statements
- Generate edge case tests

**Example Prompts:**
- "Generate xUnit tests for this class"
- "Create test cases for all validation rules in this method"
- "Write integration test for this repository method"
- "Generate edge case tests for this validation logic"

#### 8.1.3 DDD Pattern Implementation
- Generate aggregate root from requirements
- Create value objects with validation
- Implement specification pattern
- Generate domain events

**Example Prompts:**
- "Create an aggregate root for Order with invariants"
- "Generate a value object for Money with currency validation"
- "Implement specification pattern for complex query"
- "Create domain event for entity state change"

#### 8.1.4 Documentation
- Generate XML documentation comments
- Write README sections
- Create migration guides
- Generate API documentation

**Example Prompts:**
- "Add XML documentation to all public methods"
- "Write migration guide for this breaking change"
- "Generate README section for this feature"

### 8.2 Copilot Chat Best Practices

1. **Be Specific:** "Refactor this repository to use async/await consistently"
2. **Provide Context:** Include related code in the chat
3. **Iterate:** Refine generated code based on project standards
4. **Verify:** Always review and test Copilot suggestions
5. **Learn Patterns:** Study generated code to improve prompts

### 8.3 Copilot for Code Review

- Use Copilot to review code for common issues
- Ask for security vulnerability analysis
- Request performance optimization suggestions
- Get suggestions for naming improvements

---

## 9. Zero-Cost Development & Deployment Strategy

### 9.1 Development Environment (Free)

- **IDE:** Visual Studio Code (free) or Visual Studio Community (free)
- **Version Control:** GitHub (free for public repositories)
- **CI/CD:** GitHub Actions (free tier: 2,000 minutes/month)
- **Code Coverage:** Coverlet + ReportGenerator (free, open-source)
- **Static Analysis:** .NET analyzers, Roslyn analyzers (free)
- **GitHub Copilot:** Free for verified students/educators or trial

### 9.2 Testing Infrastructure (Free/Low Cost)

- **Unit Tests:** xUnit (free, open-source)
- **Mocking:** Moq (free, open-source)
- **Test Data:** AutoFixture, Bogus (free, open-source)
- **Integration Testing:** TestContainers (free, open-source)
- **Local Services:** Azurite for Azure Storage (free), LocalStack alternatives
- **Performance Testing:** BenchmarkDotNet (free, open-source)

### 9.3 NuGet Package Hosting (Free)

- **NuGet.org:** Free package hosting for open-source projects
- **GitHub Packages:** Free for public repositories
- **Package Signing:** Optional, can use free code signing tools

### 9.4 Documentation (Free)

- **GitHub Pages:** Free static site hosting for documentation
- **DocFX:** Free documentation generator for .NET
- **Markdown:** Use for README, guides, ADRs

### 9.5 Continuous Integration (GitHub Actions - Free Tier)

**Sample Workflow (.github/workflows/ci.yml):**
```yaml
name: CI/CD Pipeline

on:
  push:
    branches: [ main, develop ]
  pull_request:
    branches: [ main ]

jobs:
  build-and-test:
    runs-on: ubuntu-latest
    
    steps:
    - uses: actions/checkout@v4
    
    - name: Setup .NET 10
      uses: actions/setup-dotnet@v4
      with:
        dotnet-version: '10.0.x'
    
    - name: Restore dependencies
      run: dotnet restore
    
    - name: Build
      run: dotnet build --no-restore --configuration Release
    
    - name: Run unit tests with coverage
      run: dotnet test --no-build --configuration Release /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
    
    - name: Upload coverage to Codecov
      uses: codecov/codecov-action@v4
      with:
        files: ./coverage.opencover.xml
    
    - name: Pack NuGet packages
      if: github.ref == 'refs/heads/main'
      run: dotnet pack --no-build --configuration Release --output ./artifacts
    
    - name: Publish to NuGet
      if: github.ref == 'refs/heads/main' && github.event_name == 'push'
      run: dotnet nuget push ./artifacts/*.nupkg --api-key ${{ secrets.NUGET_API_KEY }} --source https://api.nuget.org/v3/index.json
```

### 9.6 Cost Optimization Tips

1. **Use GitHub Actions efficiently:** Cache dependencies, matrix builds
2. **Limit integration test runs:** Only on main branch or on-demand
3. **Optimize test execution:** Run unit tests first, fail fast
4. **Use lightweight containers:** Alpine-based images for Docker
5. **Leverage free tools:** Avoid paid services where free alternatives exist

---

## 10. Quality Assurance and Code Standards

### 10.1 Code Style and Formatting

#### 10.1.1 EditorConfig (.editorconfig)
```ini
root = true

[*.cs]
# Indentation and spacing
indent_style = space
indent_size = 4
tab_width = 4

# New line preferences
end_of_line = crlf
insert_final_newline = true

# File-scoped namespaces
csharp_style_namespace_declarations = file_scoped:warning

# Use var
csharp_style_var_for_built_in_types = true:suggestion
csharp_style_var_when_type_is_apparent = true:suggestion

# Expression-bodied members
csharp_style_expression_bodied_methods = when_on_single_line:suggestion
csharp_style_expression_bodied_properties = true:suggestion

# Pattern matching
csharp_style_pattern_matching_over_is_with_cast_check = true:suggestion
csharp_style_pattern_matching_over_as_with_null_check = true:suggestion

# Null checking
csharp_style_throw_expression = true:suggestion
csharp_style_conditional_delegate_call = true:suggestion

# Modifier preferences
csharp_preferred_modifier_order = public,private,protected,internal,static,extern,new,virtual,abstract,sealed,override,readonly,unsafe,volatile,async:suggestion

# Require accessibility modifiers
dotnet_style_require_accessibility_modifiers = always:warning
```

#### 10.1.2 Code Analysis Rules
```xml
<!-- Directory.Build.props -->
<Project>
  <PropertyGroup>
    <AnalysisMode>All</AnalysisMode>
    <EnforceCodeStyleInBuild>true</EnforceCodeStyleInBuild>
    <TreatWarningsAsErrors>true</TreatWarningsAsErrors>
    <Nullable>enable</Nullable>
  </PropertyGroup>
</Project>
```

### 10.2 Code Review Checklist

- [ ] Code follows DDD principles (proper aggregate boundaries, immutability)
- [ ] All public APIs have XML documentation comments
- [ ] Nullable reference types properly annotated
- [ ] No compiler warnings
- [ ] Tests cover new functionality (90%+ coverage)
- [ ] Performance considerations addressed
- [ ] Security implications reviewed
- [ ] Breaking changes documented in CHANGELOG
- [ ] Migration path provided for breaking changes

### 10.3 Pull Request Standards

**PR Template:**
```markdown
## Description
Brief description of changes

## Type of Change
- [ ] Bug fix
- [ ] New feature
- [ ] Breaking change
- [ ] Documentation update

## Checklist
- [ ] Code follows project style guidelines
- [ ] Self-review completed
- [ ] Comments added for complex logic
- [ ] Tests added/updated
- [ ] All tests pass locally
- [ ] Coverage maintained/improved
- [ ] Documentation updated
- [ ] No new compiler warnings

## Related Issues
Fixes #123
```

### 10.4 Definition of Done

A task is considered "done" when:
1. Code is written following DDD and Clean Architecture principles
2. Code uses modern C# features (.NET 10)
3. Unit tests written with 90%+ coverage
4. Integration tests written where applicable
5. All tests pass (unit + integration)
6. Code reviewed (self-review or peer review)
7. Documentation updated (XML comments, README, guides)
8. No compiler warnings or errors
9. Performance benchmarks meet targets (if applicable)
10. NuGet package builds successfully
11. Changes committed with descriptive message
12. CHANGELOG updated if user-facing change

---

## 11. Risk Management

### 11.1 Identified Risks

| Risk | Likelihood | Impact | Mitigation |
|------|------------|--------|------------|
| Breaking changes in .NET 10 | Medium | High | Thorough testing, gradual migration, compatibility layer |
| Dependency incompatibilities | Medium | High | Early audit, test each update, rollback plan |
| Extended timeline (single dev) | Medium | Medium | Use Copilot effectively, automate where possible |
| Test coverage gaps | Low | High | Systematic test creation, coverage requirements in CI |
| Existing consumers break | High | High | Versioning strategy, migration guide, deprecation notices |
| Security vulnerabilities | Low | High | Regular security audits, dependency updates, code reviews |
| Performance regression | Medium | Medium | Benchmarking before/after, performance tests in CI |

### 11.2 Contingency Plans

#### If .NET 10 migration blocked:
- Stay on .NET 8 LTS as interim step
- Apply modern C# features compatible with .NET 8
- Re-evaluate .NET 10 in 3-6 months

#### If test coverage targets not met:
- Prioritize critical path testing (80% coverage minimum)
- Create testing debt backlog
- Address in follow-up iterations

#### If breaking changes cause issues:
- Maintain dual API surface (new + legacy marked obsolete)
- Provide migration tools/scripts
- Extend support for old version

#### If timeline extends:
- Re-prioritize features (core migration first, DDD refactoring later)
- Release incremental versions (5.0-alpha, 5.0-beta, 5.1, 5.2...)
- Seek community contributions for non-critical features

---

## 12. Success Metrics

### 12.1 Technical Metrics

- [ ] **Framework:** 100% of projects on .NET 10
- [ ] **Code Modernization:** 100% file-scoped namespaces, nullable enabled
- [ ] **Test Coverage:** 90%+ overall, 100% for value objects
- [ ] **Build Time:** No regression (within 10% of baseline)
- [ ] **Package Size:** No significant increase (within 20%)
- [ ] **Performance:** No regression in benchmarks
- [ ] **Warnings:** Zero compiler warnings
- [ ] **Vulnerabilities:** Zero high/critical vulnerabilities

### 12.2 Quality Metrics

- [ ] **Code Review:** All code self-reviewed or peer-reviewed
- [ ] **Documentation:** 100% public API documented
- [ ] **Examples:** At least 5 usage examples created
- [ ] **Migration Guide:** Complete guide for consumers upgrading
- [ ] **CHANGELOG:** All changes documented
- [ ] **ADRs:** Key architectural decisions recorded

### 12.3 Delivery Metrics

- [ ] **Timeline:** Complete within 13 weeks
- [ ] **NuGet Packages:** All packages published successfully
- [ ] **GitHub Release:** Release notes published
- [ ] **Breaking Changes:** All documented with migration paths
- [ ] **Community:** No critical issues filed within 2 weeks of release

### 12.4 Adoption Metrics (Post-Release)

- NuGet download trends
- GitHub stars/forks growth
- Community contributions (issues, PRs)
- Consumer feedback (positive vs. negative)

---

## 13. Post-Release Plan

### 13.1 Immediate Post-Release (Week 14-15)

- [ ] Monitor NuGet downloads and feedback
- [ ] Address critical bugs within 24 hours
- [ ] Respond to community issues on GitHub
- [ ] Publish blog post announcing release
- [ ] Create video walkthrough of new features
- [ ] Update samples and starter templates

### 13.2 Version 5.1 (Future Enhancement)

**Potential Features:**
- Additional value objects (URL, Color, Currency)
- Enhanced specification combinators
- Improved domain event versioning
- Performance optimizations based on benchmarks
- Additional integration patterns (message bus, event store)

### 13.3 Continuous Improvement

- Monthly dependency updates via Dependabot
- Quarterly security audits
- Bi-annual .NET version alignment (as new versions release)
- Continuous documentation improvements based on user feedback
- Expand test coverage to 95%+

---

## 14. Developer Productivity Enhancements

### 14.1 IDE Setup

#### Visual Studio Code Extensions
- C# Dev Kit
- GitHub Copilot / Copilot Chat
- .NET Core Test Explorer
- Code Spell Checker
- EditorConfig for VS Code
- GitLens
- REST Client (for API testing)

#### Visual Studio Extensions
- ReSharper (optional, paid) or built-in analyzers
- NCrunch (optional, paid) or Live Unit Testing
- CodeMaid
- Productivity Power Tools

### 14.2 Automation Scripts

#### PowerShell/Bash Scripts
```powershell
# scripts/migrate-namespace.ps1
# Bulk convert to file-scoped namespaces

# scripts/update-versions.ps1
# Update all project versions

# scripts/run-all-tests.ps1
# Run unit, integration, and performance tests

# scripts/pack-nuget.ps1
# Pack all NuGet packages

# scripts/check-coverage.ps1
# Generate and display code coverage
```

### 14.3 Git Workflow

**Branching Strategy:**
- `main` - Production-ready code, .NET 10, version 5.x
- `develop` - Integration branch
- `feature/*` - Feature branches
- `bugfix/*` - Bug fix branches
- `release/*` - Release preparation branches

**Commit Message Convention:**
```
<type>(<scope>): <subject>

<body>

<footer>
```

Types: feat, fix, docs, style, refactor, test, chore

**Examples:**
- `feat(core): add file-scoped namespaces to all core projects`
- `fix(ef-core): resolve null reference in repository query`
- `test(value-objects): add tests for EmailAddress edge cases`
- `docs(readme): update installation instructions for .NET 10`

---

## 15. Knowledge Transfer and Documentation

### 15.1 Documentation Artifacts

#### Essential Documentation
- [x] README.md - Updated with .NET 10 requirements
- [ ] MIGRATION_GUIDE.md - Guide for consumers upgrading from 4.x to 5.0
- [ ] CHANGELOG.md - Detailed change log
- [ ] CONTRIBUTING.md - Guidelines for contributors
- [ ] CODE_OF_CONDUCT.md - Community standards
- [ ] ARCHITECTURE.md - Architecture overview and diagrams
- [ ] ADRs/ - Architectural Decision Records folder

#### Developer Documentation
- [ ] DEVELOPMENT.md - Local development setup
- [ ] TESTING.md - Testing guidelines and how to run tests
- [ ] RELEASE.md - Release process and checklist
- [ ] TROUBLESHOOTING.md - Common issues and solutions

#### API Documentation
- [ ] Generate API docs with DocFX
- [ ] Host on GitHub Pages
- [ ] Include code samples and tutorials

### 15.2 Samples and Examples

#### Sample Projects to Create
- [ ] **Minimal API Sample:** Using Core value objects
- [ ] **EF Core Sample:** Repository pattern usage
- [ ] **Domain Events Sample:** Event dispatching
- [ ] **Clean Architecture Sample:** Full application structure

#### Code Examples in Documentation
- Creating aggregates
- Implementing value objects
- Using specifications
- Working with repositories
- Handling domain events
- Configuring EF Core
- Using Azure Blob Service
- Sending emails with SendGrid

### 15.3 Video Tutorials (Optional)

- [ ] Getting started with EXCSLA.Shared 5.0
- [ ] Migrating from 4.x to 5.0
- [ ] Building a domain with DDD patterns
- [ ] Testing strategies for DDD applications

---

## 16. Conclusion and Next Steps

### 16.1 Summary

This PRD outlines a comprehensive plan to modernize the EXCSLA.Shared DDD Framework from .NET 6 to .NET 10, incorporating modern C# features, strengthening DDD and Clean Architecture patterns, achieving high test coverage, and deploying efficiently with zero infrastructure cost. The plan is designed for a single developer leveraging GitHub Copilot to maximize productivity.

**Key Outcomes:**
- Modern, maintainable codebase on .NET 10
- Enhanced DDD and Clean Architecture implementation
- 90%+ test coverage with comprehensive test suite
- All dependencies updated and secure
- High-quality documentation and examples
- Efficient CI/CD pipeline with zero cost
- Successful NuGet package releases

### 16.2 Immediate Next Steps

1. **Week 1:** Environment setup and dependency audit
2. **Week 2-3:** Core projects migration to .NET 10
3. **Week 4-5:** Infrastructure projects migration
4. **Week 6-7:** Establish test infrastructure and initial tests
5. **Week 8-10:** DDD refactoring and enhancements
6. **Week 11-12:** Complete test coverage
7. **Week 13:** Documentation and release

### 16.3 Long-Term Vision

EXCSLA.Shared aims to be the premier DDD framework for .NET developers building modern, maintainable, domain-centric applications. By staying current with .NET releases, adopting best practices, and maintaining high quality standards, the framework will continue to serve as a solid foundation for enterprise applications.

**Future Considerations:**
- Support for additional architectural patterns (Hexagonal, Onion)
- Integration with popular libraries (MediatR, AutoMapper)
- Cloud-native patterns (Event Sourcing, CQRS at scale)
- Microservices patterns and utilities
- Real-time features (SignalR integration)
- GraphQL support

### 16.4 Call to Action

This PRD serves as the blueprint for the modernization effort. The single developer executing this plan should:
- Review and understand each phase thoroughly
- Leverage GitHub Copilot extensively for code generation and testing
- Maintain discipline with test coverage and documentation
- Communicate progress regularly via GitHub issues/discussions
- Seek community feedback during beta releases
- Celebrate milestones along the way!

---

## Appendix A: Reference Links

### Official Documentation
- [.NET 10 Documentation](https://docs.microsoft.com/en-us/dotnet/)
- [C# Language Reference](https://docs.microsoft.com/en-us/dotnet/csharp/)
- [Entity Framework Core 10](https://docs.microsoft.com/en-us/ef/core/)
- [xUnit Documentation](https://xunit.net/)
- [GitHub Actions](https://docs.github.com/en/actions)

### DDD Resources
- Eric Evans - Domain-Driven Design: Tackling Complexity in the Heart of Software
- Vaughn Vernon - Implementing Domain-Driven Design
- Martin Fowler - Patterns of Enterprise Application Architecture
- [DDD Community](https://www.domainlanguage.com/)

### Tools and Libraries
- [dotnet-outdated](https://github.com/dotnet-outdated/dotnet-outdated)
- [Coverlet](https://github.com/coverlet-coverage/coverlet)
- [ReportGenerator](https://github.com/danielpalme/ReportGenerator)
- [BenchmarkDotNet](https://benchmarkdotnet.org/)
- [TestContainers](https://www.testcontainers.org/)
- [FluentAssertions](https://fluentassertions.com/)

---

## Appendix B: Glossary

- **Aggregate:** A cluster of domain objects treated as a single unit
- **Aggregate Root:** The entry point to an aggregate, ensuring consistency
- **Clean Architecture:** Architecture that separates concerns into layers with dependency rules
- **CQRS:** Command Query Responsibility Segregation
- **DDD:** Domain-Driven Design
- **Domain Event:** Something that happened in the domain that domain experts care about
- **Entity:** An object with identity and lifecycle
- **Repository:** Abstraction for data access, works with aggregates
- **Specification:** Pattern for encapsulating query logic
- **Value Object:** An immutable object defined by its attributes, not identity
- **Ubiquitous Language:** Shared language between developers and domain experts

---

**Document Version:** 1.0  
**Last Updated:** February 2026  
**Status:** Approved for Implementation  
**Approved By:** Executive Computer Systems, LLC

---

*This PRD is a living document and may be updated as the project progresses and new requirements emerge.*