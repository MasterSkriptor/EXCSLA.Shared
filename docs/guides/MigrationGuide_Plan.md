# Migration Guide: Core→Domain Breaking Change

This guide explains the breaking change from the Core to Domain structure, migration steps, and references for updating your projects.

## Overview
- The Domain layer now replaces the previous Core layer.
- All domain models, value objects, and related logic should be moved to the Domain folder.

## Migration Steps
1. Move all domain-related files from Core to Domain.
2. Update all references in your codebase from Core to Domain.
3. Adjust your project and solution files to reference the new Domain structure.
4. Run tests to ensure everything works as expected.

## References
- See the updated docs/README.md for the new documentation structure.
- For more details, refer to the plans and reference folders in docs.
