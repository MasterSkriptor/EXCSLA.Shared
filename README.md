
# EXCSLA.Shared
Domain Driven Design Domain and Framework for EXCSLA designed websites. Created to deploy to nuget and can be used for modern ddd designed websites and apis.

# Domain
The Domain project will contain any shared entities, aggregates, and events that are needed on most or all websites / apis created by EXCSLA.

# Domain.ValueObjects.Common
The Domain.ValueObjects.Common will contain any common values types needed on most or all websites / apis created by EXCSLA. These value objects can be added to Aggregates or Entities by simply referencing EXCSLA.Shared.Domain.ValueObjects.Common and adding them as a property. Because they are value objects they are immutable, so you will need to add a new object instead of changing their properties. Also steps will need to be taken to properly deal with them in any ORM you may use. For EF Core you will need to add OwnsOne, and optionally name the column name, in order to add them as columns in your database.

# Framework
The framework project will contain any boiler plate code needed for framework services and data access used by EXCSLA. This section may entail multiple projects and separate Nuget packages so that services not needed by any particular site do not have to reference them.

# EXCSLA
EXCSLA stands for Executive Computer Systems, LLC and is a Louisiana based IT Firm.
