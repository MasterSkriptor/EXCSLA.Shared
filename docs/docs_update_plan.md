## Project File Cleanup, Docs Organization & Breaking Changes

This plan ensures only the root-level README.md is moved; README.md files within individual projects or subfolders remain in place. The docs folder will be organized for clarity, and documentation will be updated for the Core→Domain breaking change.

**Steps**
1. **Create a structured hierarchy under docs:**
   - docs/
     - guides/
     - issues/
     - release_notes/
     - plans/
     - reference/
     - legacy/
     - README.md (main entry point for docs)
2. **Move all .md files from the project root (except LICENSE and README.md files inside subfolders) into the appropriate docs subfolders.**
   - Only the root README.md is moved to docs/README.md.
   - All other README.md files in subfolders (e.g., src/, examples/, docs/) remain in their respective locations.
3. **Remove the tempMigrationScripts folder and all its contents.**
4. **Review and update documentation:**
   - Update all references and links to match the new docs structure.
   - Add or update guides/MigrationGuide_Plan.md to explain the Core→Domain breaking change, including migration steps and references.
   - Update docs/README.md with a table of contents and folder explanations.
5. **Check for and fix any broken links or outdated references in all documentation.**
6. **Optionally, update the main project README (now docs/README.md) to direct users to the new documentation structure and highlight the migration/breaking changes guide.**

**Verification**
- Only the root README.md is moved; all other README.md files remain in their original locations.
- All other .md files (except LICENSE) are organized under docs/ as per the new structure.
- tempMigrationScripts is fully removed.
- docs/README.md provides a clear table of contents and folder explanations.
- All documentation links are updated and functional.
- The migration guide fully explains the Core→Domain breaking change.

**Decisions**
- README.md files in subfolders are not moved.
- LICENSE remains in the root.
- docs/ is organized for clarity and maintainability.
