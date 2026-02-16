using System.Collections.Generic;
using System.Threading.Tasks;
using EXCSLA.Shared.Domain;

namespace EXCSLA.Shared.Domain.Interfaces;

/// <summary>
/// Generic async repository interface for data access operations on aggregate root entities.
/// Provides CRUD operations and specification-based querying for filtering, sorting, and pagination.
/// </summary>
/// <typeparam name="T">The type of entity this repository manages. Must derive from BaseEntity.</typeparam>
/// <remarks>
/// This interface is designed to work with specifications (ISpecification&lt;T&gt;) to provide
/// a flexible query mechanism without exposing the underlying ORM (e.g., Entity Framework Core).
/// Implementations should handle all data access concerns including eager loading,
/// filtering, sorting, and pagination.
/// </remarks>
public interface IAsyncRepository<T> where T : BaseEntity
{
    /// <summary>
    /// Retrieves an entity by its primary key asynchronously.
    /// </summary>
    /// <param name="id">The primary key value of the entity to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation with the entity result, or null if not found.</returns>
    Task<T> GetByIdAsync(int id);

    /// <summary>
    /// Retrieves all entities of type T asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation with all entities.</returns>
    Task<List<T>> ListAllAsync();

    /// <summary>
    /// Retrieves entities matching the specified specification asynchronously.
    /// </summary>
    /// <param name="spec">The specification that defines filtering, sorting, and pagination criteria.</param>
    /// <returns>A task that represents the asynchronous operation with entities matching the specification.</returns>
    Task<List<T>> ListAsync(ISpecification<T> spec);

    /// <summary>
    /// Adds a new entity to the repository asynchronously.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <returns>A task that represents the asynchronous operation with the added entity (Id assigned).</returns>
    Task<T> AddAsync(T entity);

    /// <summary>
    /// Updates an existing entity in the repository asynchronously.
    /// </summary>
    /// <param name="entity">The entity with updated values.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task UpdateAsync(T entity);

    /// <summary>
    /// Deletes an entity from the repository asynchronously.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DeleteAsync(T entity);

    /// <summary>
    /// Counts entities matching the specified specification asynchronously.
    /// </summary>
    /// <param name="spec">The specification that defines filtering criteria.</param>
    /// <returns>A task that represents the asynchronous operation with the count of matching entities.</returns>
    Task<int> CountAsync(ISpecification<T> spec);
}