# Phase 3: Persistence Layer Integration - Planning & Roadmap

**Status:** 📋 PLANNING  
**Target Start:** February 6, 2026  
**Estimated Duration:** 3-4 weeks  
**Phase Goal:** Implement EntityFramework Core integration and repository pattern

---

## Phase Overview

Phase 3 focuses on implementing the persistence layer using EntityFramework Core, completing the DDD (Domain-Driven Design) pattern implementation started in Phase 1.

### Key Objectives

1. ✅ Implement `IAsyncRepository<T>` with EntityFramework Core
2. ✅ Create database context and migrations
3. ✅ Implement Unit of Work pattern
4. ✅ Add specifications pattern support to queries
5. ✅ Comprehensive persistence testing
6. ✅ Production-ready migration strategy

### Success Criteria

- [ ] EntityFramework Core integration complete
- [ ] 50+ persistence layer tests
- [ ] Database migration support
- [ ] 100% API documentation
- [ ] Production deployment guide
- [ ] Zero security issues

---

## Phase 3 Milestones

### Milestone 1: Infrastructure & Data Context (Week 1)

**Objective:** Set up EF Core infrastructure and database context

**Deliverables:**
- Create `EXCSLA.Shared.Persistence` project (.NET 10)
- Implement `ExcslaDbContext` with DbSet<T> mappings
- Configure aggregate root mappings
- Set up connection string handling
- Create migration infrastructure
- Add test database context (SQLite in-memory)

**Files to Create:**
```
src/Persistence/
├── EXCSLA.Shared.Persistence.csproj
├── Data/
│   ├── ExcslaDbContext.cs
│   ├── EntityConfigurations/
│   │   └── AggregateRootConfiguration.cs
│   └── Migrations/
│       └── InitialCreate.cs
├── README.md (700+ lines)
└── Examples/
    └── ContextConfiguration.cs
```

**Tests to Create:**
```
Tests/Persistence/
├── ExcslaDbContextShould.cs (15+ tests)
├── MigrationShould.cs (10+ tests)
└── DatabaseInitializationShould.cs (10+ tests)
```

**Estimated Effort:** 3-4 days

---

### Milestone 2: Repository Pattern Implementation (Week 1-2)

**Objective:** Implement generic repository and unit of work patterns

**Deliverables:**
- Generic `AsyncRepository<TEntity>` implementation
- `UnitOfWork` orchestrator
- Transaction management
- Specification pattern integration
- Lazy loading vs eager loading patterns
- Query optimization (AsNoTracking)

**Files to Create:**
```
src/Persistence/
├── Repositories/
│   ├── AsyncRepository.cs
│   ├── IUnitOfWork.cs
│   └── UnitOfWork.cs
├── ServiceCollectionExtensions.cs
└── Documentation/
    └── PatternGuide.md
```

**Tests to Create:**
```
Tests/Persistence/
├── AsyncRepositoryShould.cs (20+ tests)
├── UnitOfWorkShould.cs (15+ tests)
├── SpecificationIntegrationShould.cs (15+ tests)
└── TransactionManagementShould.cs (10+ tests)
```

**Estimated Effort:** 4-5 days

---

### Milestone 3: Migration & Seeding Strategy (Week 2)

**Objective:** Implement database migration and data seeding

**Deliverables:**
- Migration scaffolding guide
- Data seeding patterns
- Environment-specific configurations
- Migration versioning
- Rollback procedures
- Production deployment strategy

**Files to Create:**
```
src/Persistence/
├── Migrations/
│   ├── MigrationRunner.cs
│   ├── SeedData.cs
│   └── EnvironmentSpecificSeed.cs
├── Database/
│   └── InitializationScript.sql
└── Documentation/
    └── MigrationGuide.md (500+ lines)
```

**Tests to Create:**
```
Tests/Persistence/
├── MigrationRunnerShould.cs (12+ tests)
├── SeedingShould.cs (15+ tests)
└── EnvironmentConfigurationShould.cs (10+ tests)
```

**Estimated Effort:** 3-4 days

---

### Milestone 4: Real-World Scenarios & Testing (Week 3)

**Objective:** Integration tests and real-world usage patterns

**Deliverables:**
- Integration tests with real database
- Performance testing patterns
- Concurrency scenario testing
- Error handling and recovery
- Best practices documentation
- Configuration examples

**Files to Create:**
```
Tests/Persistence/
├── IntegrationTests/
│   ├── PersistenceIntegrationShould.cs (25+ tests)
│   ├── ConcurrencyScenarioShould.cs (15+ tests)
│   └── PerformancePatternShould.cs (10+ tests)
└── TestData/
    └── SampleAggregates.cs
```

**Estimated Effort:** 3-4 days

---

### Milestone 5: Documentation & Release (Week 3-4)

**Objective:** Complete documentation and prepare release

**Deliverables:**
- Comprehensive README (800+ lines)
- Migration guide
- Best practices documentation
- Configuration patterns
- Troubleshooting guide
- Example applications
- CHANGELOG update
- NuGet package preparation

**Files to Create:**
```
src/Persistence/README.md (800+ lines)
Documentation/
├── EntityMapping.md (300+ lines)
├── QueryPatterns.md (300+ lines)
├── MigrationStrategies.md (200+ lines)
└── ProductionDeployment.md (200+ lines)
```

**Estimated Effort:** 3-4 days

---

## Technical Specifications

### EntityFramework Core Version
- **Version:** 10.0 (Latest)
- **Target Framework:** .NET 10
- **Database Support:** 
  - Development: SQLite (in-memory for tests)
  - Production: SQL Server (recommended)
  - Alternative: PostgreSQL, MySQL supported

### Dependencies
```xml
<ItemGroup>
  <PackageReference Include="Microsoft.EntityFrameworkCore" Version="10.0" />
  <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="10.0" />
  <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="10.0" />
  <PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="10.0" /> <!-- Tests only -->
  <PackageReference Include="Microsoft.EntityFrameworkCore.InMemory" Version="10.0" /> <!-- Tests only -->
</ItemGroup>
```

### Architecture Pattern

```
Persistence Layer (Phase 3)
├── DbContext (ExcslaDbContext)
│   ├── DbSet<TEntity> properties
│   ├── Entity configurations
│   └── Change tracking
├── Repository Pattern (AsyncRepository<T>)
│   ├── CRUD operations
│   ├── Specification support
│   └── Query optimization
├── Unit of Work (UnitOfWork)
│   ├── Transaction management
│   ├── Repository coordination
│   └── SaveChangesAsync()
└── Migrations
    ├── Schema management
    ├── Data seeding
    └── Versioning
```

### Database Schema Strategy

**Conventions Over Configuration:**
- Use EF Core conventions for standard entities
- Explicit Fluent API configuration for complex mappings
- Shadow properties for audit fields (CreatedAt, UpdatedAt)
- Soft deletes via query filters

**Aggregate Root Mapping:**
- Aggregate boundaries enforced at DbContext level
- Value objects mapped as owned types
- Domain event storage (if needed)
- Optimistic concurrency control

---

## Testing Strategy

### Unit Tests (20+ tests)
- Repository CRUD operations
- Specification query building
- Unit of Work transaction handling
- Configuration validation

### Integration Tests (30+ tests)
- Real database operations
- Migration execution
- Concurrency scenarios
- Query performance
- Transaction rollback

### Performance Tests (10+ tests)
- Query optimization verification
- Eager loading patterns
- Lazy loading implications
- Connection pooling

---

## Risk Assessment

### Identified Risks

| Risk | Probability | Impact | Mitigation |
|------|------------|--------|-----------|
| EF Core migration issues | Medium | High | Early testing with SQL Server |
| Query performance problems | Medium | High | Add indexes, use AsNoTracking |
| Concurrency conflicts | Low | Medium | Optimistic concurrency testing |
| Model mapping complexity | Low | Medium | Clear configuration patterns |

### Mitigation Strategies

1. **Early SQL Server Testing**
   - Test migrations on real SQL Server early
   - Verify query performance
   - Check index generation

2. **Query Optimization**
   - Analyze generated SQL (EXPLAIN)
   - Test with realistic data volumes
   - Profile performance scenarios

3. **Comprehensive Testing**
   - Unit tests for repositories
   - Integration tests with real DB
   - Concurrency scenario testing

---

## Dependencies & Prerequisites

### External Dependencies
- ✅ EntityFramework Core 10.0
- ✅ SQL Server/PostgreSQL/MySQL drivers
- ✅ Microsoft.EntityFrameworkCore.Tools (for migrations)

### Internal Dependencies
- ✅ Phase 1 - Domain & Application layers (v5.0.0)
- ✅ Phase 2 - Infrastructure layer (v5.1.0 + v5.2.0)
- ✅ IAsyncRepository<T> abstraction (defined in Phase 1)
- ✅ ISpecification<T> pattern (defined in Phase 1)

### Package Version Strategy

```
Phase 3 Release Versions:
- Persistence Layer: v6.0.0 (major version due to new layer)
- Core Layer: v5.1.0 (stable, no changes)
- Application Layer: v5.1.0 (stable, no changes)
- Infrastructure Layer: v5.2.0 (no changes)
```

---

## Deliverables Checklist

### Code
- [ ] ExcslaDbContext implementation
- [ ] AsyncRepository<T> implementation
- [ ] UnitOfWork orchestrator
- [ ] Entity configurations
- [ ] Migration infrastructure
- [ ] Service collection extensions

### Testing
- [ ] 20+ unit tests
- [ ] 30+ integration tests
- [ ] 10+ performance tests
- [ ] 100% test pass rate
- [ ] Coverage >= 90%

### Documentation
- [ ] README.md (800+ lines)
- [ ] Entity mapping guide
- [ ] Query pattern examples
- [ ] Migration strategy
- [ ] Production deployment guide
- [ ] Troubleshooting guide

### Quality
- [ ] 0 critical errors
- [ ] Security audit passed
- [ ] 100% API documentation
- [ ] Build successful
- [ ] All warnings resolved

### Release
- [ ] CHANGELOG updated
- [ ] NuGet packages generated
- [ ] GitHub release created
- [ ] Packages published

---

## Next Phase Preview

**Phase 4: Advanced Features & Polish**
- [ ] Caching layer abstraction
- [ ] Logging abstractions
- [ ] Health check patterns
- [ ] Advanced query patterns
- [ ] Performance benchmarking
- [ ] Production deployment automation

---

## Timeline

| Phase | Duration | Status |
|-------|----------|--------|
| Phase 1 - MVP | Complete | ✅ |
| Phase 2 - Infrastructure | Complete | ✅ |
| **Phase 3 - Persistence** | **3-4 weeks** | 📋 Planning |
| Phase 4 - Advanced | TBD | 🔮 Future |
| Phase 5 - Production | TBD | 🔮 Future |

---

## Estimated Effort

| Milestone | Effort | Status |
|-----------|--------|--------|
| 1. Data Context | 3-4 days | 📋 Ready |
| 2. Repository Pattern | 4-5 days | 📋 Ready |
| 3. Migration & Seeding | 3-4 days | 📋 Ready |
| 4. Real-World Scenarios | 3-4 days | 📋 Ready |
| 5. Documentation & Release | 3-4 days | 📋 Ready |
| **Total Phase 3** | **16-21 days** | 📋 Ready |

---

## Resources

### Documentation
- [EntityFramework Core Documentation](https://learn.microsoft.com/en-us/ef/core/)
- [DDD Repository Pattern](https://martinfowler.com/eaaCatalog/repository.html)
- [Unit of Work Pattern](https://martinfowler.com/eaaCatalog/unitOfWork.html)
- [Specification Pattern](https://martinfowler.com/applyingDdd/findingObjects.html)

### Tools
- `dotnet ef migrations` - Migration management
- `dotnet ef database` - Database operations
- SQL Server LocalDB - Local development
- SSMS or Azure Data Studio - Database visualization

---

## Success Metrics

**On Completion of Phase 3:**
- ✅ Complete persistence layer implemented
- ✅ 60+ tests (30+ integration tests)
- ✅ Zero critical issues
- ✅ 100% API documentation
- ✅ Production-ready migration strategy
- ✅ 3 NuGet packages published
- ✅ GitHub releases published
- ✅ Ready for Phase 4

---

**Phase 3 Status:** 📋 **READY TO START**

Target Start Date: February 6, 2026  
Estimated Completion: March 6, 2026
