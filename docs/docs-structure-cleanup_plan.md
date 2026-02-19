1. Task: Create Feature Branch
   * Create a new feature branch from dev for this documentation and project file reorganization.
   * Name the branch using convention: feature/docs-structure-cleanup.
   * Verify: Confirm branch is created and checked out.

2. Task: Create Structured Docs Hierarchy
   * Create the new folder structure under docs: guides, issues, release_notes, plans, reference, legacy, and a main README.md for docs.
   * Verify Build: Run dotnet build.

[x] Task 2 complete

3. Task: Move Root Markdown Files to Docs
   * Move all .md files from the project root (except LICENSE and README.md files inside subfolders) into the appropriate docs subfolders.
   * Only move the root README.md to docs/README.md.
   * Verify Build: Run dotnet build.
[x] Task 3 complete

4. Task: Remove tempMigrationScripts Folder [x]
   * Delete the tempMigrationScripts folder and all its contents from the project.
   * Verify Build: Run dotnet build.

5. Task: Update Documentation Links and References [x]
   * Update all documentation references and links to match the new docs structure.
   * Verify Build: Run dotnet build.


6. Task: Add/Update Migration Guide for Core→Domain Change [x]
   * Add or update guides/MigrationGuide_Plan.md to explain the Core→Domain breaking change, including migration steps and references.
   * Verify Build: Run dotnet build.

7. Task: Update docs/README.md with Table of Contents
   * Update docs/README.md to include a table of contents and folder explanations for the new docs structure.
   * Verify Build: Run dotnet build.

8. Task: Fix Broken Links and Outdated References
   * Check for and fix any broken links or outdated references in all documentation files.
   * Verify Build: Run dotnet build.

9. Task: Update Main Project README to Highlight Changes
   * Optionally, update the main project README (now docs/README.md) to direct users to the new documentation structure and highlight the migration/breaking changes guide.
   * Verify Build: Run dotnet build.

10. Task: Verify All Tests Pass
    * Run dotnet test to ensure all tests pass after documentation and file changes.
    * Verify Tests: Run dotnet test.
