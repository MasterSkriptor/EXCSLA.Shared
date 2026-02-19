---
### [x] **Implementing Task 1: Create Feature Branch**

**File:** N/A
**Action:** `run_shell_command`

**Command:**
```
git checkout -b feature/docs-structure-cleanup dev
```

**Agent Prompt:**
"Create a new feature branch from dev named 'feature/docs-structure-cleanup'."

---
### [x] **Implementing Task 2: Create Structured Docs Hierarchy**

**File:** docs/
**Action:** `run_shell_command`

**Command:**
```
mkdir -p docs/guides docs/issues docs/release_notes docs/plans docs/reference docs/legacy && touch docs/README.md
```

**Agent Prompt:**
"Create the new folder structure under docs and add a main README.md file."

---
### [ ] **Verifying Task 2**

**File:** N/A
**Action:** `run_shell_command`

**Command:**
```
dotnet build
```

**Agent Prompt:**
"Run dotnet build to verify the project compiles after creating the docs folder structure."

---
### [x] **Implementing Task 3: Move Root Markdown Files to Docs**

**File:** docs/
**Action:** `run_shell_command`

**Command:**
```
find . -maxdepth 1 -name '*.md' ! -name 'LICENSE' ! -name 'README.md' -exec mv {} docs/ \; && mv README.md docs/README.md
```

**Agent Prompt:**
"Move all .md files from the project root (except LICENSE and README.md files inside subfolders) into the appropriate docs subfolders, and move the root README.md to docs/README.md."

---
### [ ] **Verifying Task 3**

**File:** N/A
**Action:** `run_shell_command`

**Command:**
```
dotnet build
```

**Agent Prompt:**
"Run dotnet build to verify the project compiles after moving markdown files."

---
### [x] **Implementing Task 4: Remove tempMigrationScripts Folder**

**File:** tempMigrationScripts/
**Action:** `run_shell_command`

**Command:**
```
rm -rf tempMigrationScripts
```

**Agent Prompt:**
"Delete the tempMigrationScripts folder and all its contents from the project."

---
### [ ] **Verifying Task 4**

**File:** N/A
**Action:** `run_shell_command`

**Command:**
```
dotnet build
```

**Agent Prompt:**
"Run dotnet build to verify the project compiles after removing tempMigrationScripts."

---
### [ ] **Implementing Task 5: Update Documentation Links and References**

**File:** docs/
**Action:** `run_shell_command`

**Command:**
```
grep -rl '\.md' docs/ | xargs sed -i 's|(../|(/docs/|g'
```

**Agent Prompt:**
"Update all documentation references and links to match the new docs structure."

---
### [ ] **Verifying Task 5**

**File:** N/A
**Action:** `run_shell_command`

**Command:**
```
dotnet build
```

**Agent Prompt:**
"Run dotnet build to verify the project compiles after updating documentation links."

---
### [ ] **Implementing Task 6: Add/Update Migration Guide for Core→Domain Change**

**File:** docs/guides/MigrationGuide_Plan.md
**Action:** `write_file`

**Content:**
```
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
```

**Agent Prompt:**
"Add or update guides/MigrationGuide_Plan.md to explain the Core→Domain breaking change, including migration steps and references."

---
### [ ] **Verifying Task 6**

**File:** N/A
**Action:** `run_shell_command`

**Command:**
```
dotnet build
```

**Agent Prompt:**
"Run dotnet build to verify the project compiles after adding the migration guide."

---
### [ ] **Implementing Task 7: Update docs/README.md with Table of Contents**

**File:** docs/README.md
**Action:** `replace`

**Content:**
```
# EXCSLA.Shared Documentation

## Table of Contents
- [Guides](guides/)
- [Issues](issues/)
- [Release Notes](release_notes/)
- [Plans](plans/)
- [Reference](reference/)
- [Legacy](legacy/)

## Folder Explanations
- **guides/**: How-tos, migration guides, and best practices
- **issues/**: Issue tracking and documentation
- **release_notes/**: Release notes and changelogs
- **plans/**: Project and documentation plans
- **reference/**: API and technical references
- **legacy/**: Archived or legacy documentation

For migration details, see [guides/MigrationGuide_Plan.md](guides/MigrationGuide_Plan.md).
```

**Agent Prompt:**
"Update docs/README.md to include a table of contents and folder explanations for the new docs structure."

---
### [ ] **Verifying Task 7**

**File:** N/A
**Action:** `run_shell_command`

**Command:**
```
dotnet build
```

**Agent Prompt:**
"Run dotnet build to verify the project compiles after updating docs/README.md."

---
### [ ] **Implementing Task 8: Fix Broken Links and Outdated References**

**File:** docs/
**Action:** `run_shell_command`

**Command:**
```
grep -r '\[.*\](.*\.md)' docs/ | grep -v 'docs/'
```

**Agent Prompt:**
"Check for and fix any broken links or outdated references in all documentation files."

---
### [ ] **Verifying Task 8**

**File:** N/A
**Action:** `run_shell_command`

**Command:**
```
dotnet build
```

**Agent Prompt:**
"Run dotnet build to verify the project compiles after fixing broken links."

---
### [ ] **Implementing Task 9: Update Main Project README to Highlight Changes**

**File:** docs/README.md
**Action:** `replace`

**Content:**
```
# EXCSLA.Shared Documentation

## Table of Contents
- [Guides](guides/)
- [Issues](issues/)
- [Release Notes](release_notes/)
- [Plans](plans/)
- [Reference](reference/)
- [Legacy](legacy/)

## Migration Notice
A major breaking change has moved all domain logic from Core to Domain. See [guides/MigrationGuide_Plan.md](guides/MigrationGuide_Plan.md) for migration steps and details.

## Folder Explanations
- **guides/**: How-tos, migration guides, and best practices
- **issues/**: Issue tracking and documentation
- **release_notes/**: Release notes and changelogs
- **plans/**: Project and documentation plans
- **reference/**: API and technical references
- **legacy/**: Archived or legacy documentation
```

**Agent Prompt:**
"Update the main project README (now docs/README.md) to direct users to the new documentation structure and highlight the migration/breaking changes guide."

---
### [ ] **Verifying Task 9**

**File:** N/A
**Action:** `run_shell_command`

**Command:**
```
dotnet build
```

**Agent Prompt:**
"Run dotnet build to verify the project compiles after updating the main README."

---
### [ ] **Implementing Task 10: Verify All Tests Pass**

**File:** N/A
**Action:** `run_shell_command`

**Command:**
```
dotnet test
```

**Agent Prompt:**
"Run dotnet test to ensure all tests pass after documentation and file changes."

---
