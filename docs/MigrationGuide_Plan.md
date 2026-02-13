

## Clean Architecture Refactor Plan

### Goal
Restructure EXCSLA.Shared to fully align with Clean Architecture principles, improve modularity, and enhance usability for consumers, using Copilot Pro and agents for maximum efficiency.

### Phases & Steps

---
## Migration Completion Summary (2026-02-10)
The migration process for EXCSLA.Shared has been completed successfully. All core components (Application, Core, Infrastructure, UI) have been migrated, validated, and tested. Documentation and references have been updated to reflect the new structure and Clean Architecture alignment.

**Migration Steps Completed:**
- Reviewed requirements and migration plan
- Identified files and components to migrate
- Prepared migration scripts and steps
- Executed migration for core components
- Validated migrated components (build and tests succeeded)
- Updated documentation and references

For further details, see the updated README and Migration.md files.

---

#### Preparation
1. **Plan Folder/Module/Package Reorganization**
	- Review current structure and dependencies.
	- Communicate plan to contributors.
	- Assign independent tasks (e.g., folder reorg, Dispatcher pipeline, DI onboarding, CQRS sample, docs) to Copilot agents for parallel execution where possible.
2. **Create Migration.md**
	- Summarize breaking changes and upgrade steps for users.

#### Refactor (Parallelize with Agents)
3. **Reorganize Folders by Layer**
	- Move Domain, Application, Infrastructure, and Dispatcher into their own top-level folders under `src/`.
	- Prompt: "Move all domain models, value objects, and domain services to src/Domain. Move CQRS interfaces and abstractions to src/Application. Move data access and external service implementations to src/Infrastructure. Move Dispatcher and pipeline behaviors to src/Dispatcher."
	- Assign to agent; review output before merging.
	- Update and run tests after this step.
4. **Split into NuGet Packages (Optional)**
	- Create separate .csproj files for each layer/module for independent packaging: Domain, Application, Infrastructure, Dispatcher.
	- Prompt: "Create a .csproj for each layer/module and update solution and package metadata."
	- Assign to agent; review output before merging.
	- Update and run tests after this step.
5. **Make Dispatcher a First-Class Module**
	- Refactor Dispatcher and pipeline behaviors into their own folder/module.
	- Prompt: "Move Dispatcher and all pipeline-related code into src/Dispatcher and ensure it is self-contained."
	- Assign to agent; review output before merging.
	- Update and run tests after this step.

#### Validation & Automation
6. **Add Minimal CQRS Example**
	- Create an `examples/MinimalCQRS/` folder with a simple project demonstrating both command and query flows, Dispatcher usage, pipeline behaviors, and a test project.
	- Prompt: "Add a minimal CQRS sample project showing command, query, handler, Dispatcher, and pipeline behaviors in action."
	- Assign to agent; review output before merging.
	- Update and run tests after this step.
7. **Centralize DI Registration**
	- Provide a single extension method (e.g., `services.AddExcSlaShared()`) that registers all core services, Dispatcher, and default pipeline behaviors.
	- Prompt: "Implement a DI extension method that registers all EXCSLA.Shared services and behaviors for easy onboarding."
	- Assign to agent; review output before merging.
	- Update and run tests after this step.
8. **Automate Quality Gates**
	- Set up CI/CD to run tests and linting after each push.
	- Automate checks for documentation formatting, broken links, and code snippet validity.

#### Documentation & Review
9. **Update Documentation**
	- Revise all guides and onboarding docs in `docs/` to reflect the new structure and usage patterns.
	- Add cross-links between README, Migration Guide, and Migration.md.
	- Prompt: "Update all documentation to match the new folder structure, DI onboarding, and example usage."
	- Assign to agent; review output before merging.
10. **Review & Feedback**
	- Solicit feedback from users and contributors on the new structure and onboarding experience.
	- Review ISSUES files and update the plan as needed.
	- Address usability or onboarding issues before finalizing the migration.

### Copilot/Copilot Pro Best Practices
- Use explicit, scoped prompts for each task.
- Parallelize independent tasks with agents for speed.
- Always review agent/Copilot output before merging.
- Use Copilot to generate and validate tests after each refactor.
- Automate quality checks (CI/CD, lint, docs).
- Keep Migration.md and CHANGELOG.md updated as you progress.
- Iterate and adapt the plan as you learn from each phase.

### Notes
- Ensure all code compiles and passes tests after each major refactor step.
- Communicate breaking changes and migration steps in the documentation (Migration.md).
## Dispatcher Pipeline Enhancement Plan

### Goal
Update the Dispatcher to support pipeline behaviors (middleware) for validation, logging, and error handling, matching MediatR's extensibility and Clean Architecture best practices.

### Steps
1. **Design IPipelineBehavior<TRequest, TResponse> interface**
	- Prompt: "Design an interface for pipeline behaviors that can wrap command/query handling logic, similar to MediatR's IPipelineBehavior."
2. **Implement example behaviors** (e.g., ValidationBehavior, LoggingBehavior, ErrorHandlingBehavior)
	- Prompt: "Implement a validation pipeline behavior that runs before the handler and throws on validation errors."
	- Prompt: "Implement a logging pipeline behavior that logs before and after handler execution."
	- Prompt: "Implement an error handling pipeline behavior that catches and logs exceptions."
3. **Update Dispatcher to resolve and execute pipeline behaviors in order**
	- Prompt: "Refactor the Dispatcher so that it composes and executes all registered pipeline behaviors before invoking the handler. Behaviors should be resolved from DI."
4. **Register pipeline behaviors in DI**
	- Prompt: "Show how to register multiple pipeline behaviors in the DI container so they are applied in order."
5. **Write tests and usage examples**
	- Prompt: "Write a unit test that verifies the order and effect of multiple pipeline behaviors in the Dispatcher."
	- Prompt: "Show a usage example with validation, logging, and error handling behaviors applied to a command."

### Notes
- Ensure the pipeline is generic and works for both commands and queries.
- Behaviors should be composable and order-dependent.
- Document how to add custom behaviors for other cross-cutting concerns.
# Migration Guide Documentation Plan

## Context
- Audience: Developers migrating from older EXCSLA versions or other CQRS frameworks
- Goal: Enable smooth migration to EXCSLA.Shared (Dispatcher, .NET 10)
- Requirements: Dispatcher adoption, .NET 10 project updates, troubleshooting
- References: MIGRATION.md, EXCSLA repo

## Plan Steps
1. Clearly state that EXCSLA.Shared provides its own MediatR-like Dispatcher implementation, supporting pipelines for validation, logging, and error handling, following modern Clean Architecture paradigms.
2. Show Dispatcher refactoring for projects migrating from older EXCSLA versions or other CQRS frameworks.
3. Guide .NET 10 project file updates.
4. List common pitfalls and troubleshooting.
5. Provide Copilot-friendly checklists.
6. Update or create a CHANGELOG.md for documentation changes.
7. Ensure all code compiles and passes tests before pushing to GitHub
7. Specify review/approval workflow for documentation changes.
8. Verify all code snippets build and run as shown.
9. Ensure documentation accessibility (headings, alt text, tables).
10. Collect feedback from users and Copilot after major updates.
11. Archive or link to old documentation versions as needed.
12. (Optional) Automate checks for formatting, broken links, and code snippet validity.
