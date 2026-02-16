# EXCSLA.Shared.Domain

## Overview
EXCSLA.Shared.Domain is the domain layer of the EXCSLA.Shared DDD Framework. It provides foundational building blocks for enterprise applications using Domain-Driven Design (DDD) and Clean Architecture principles. This replaces the previous 'Core' naming convention.

## Features
- Aggregates, Entities, and Value Objects
- Domain Events and Event Dispatching
- Guard Clauses and Specifications
- Custom Domain Exceptions
- Modular, testable, and extensible design

## Purpose
This package enables you to model your business logic and rules in a clean, maintainable way. It is intended for use as the domain of your application, with no dependencies on infrastructure or application layers.

## Getting Started
1. Install the NuGet package:
   ```shell
   dotnet add package EXCSLA.Shared.Domain
   ```
2. Reference the project in your solution.
3. Start modeling your domain using aggregates, entities, value objects, and domain events.

## Example Structure
```
Domain/
├── Aggregates/
├── Entities/
├── ValueObjects/
├── Events/
├── GuardClauses/
├── Specifications/
└── Exceptions/
```

## License
LGPL-3.0-or-later

## Contributing
This is an open source project. Contributions are welcome! Please submit a pull request on GitHub:
https://github.com/MasterSkriptor/EXCSLA.Shared

## Authors
H.E. Collins
Executive Computer Systems, LLC
