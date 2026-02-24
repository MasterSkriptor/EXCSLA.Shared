using System.Collections.Generic;
using System.Threading.Tasks;
using EXCSLA.Shared.Domain;

namespace EXCSLA.Shared.Domain.Interfaces;

/// <summary>
/// Generic async repository interface for data access operations on aggregate root entities.
/// Provides CRUD operations and specification-based querying for filtering, sorting, and pagination.
/// </summary>
/// <typeparam name="T">The type of entity this repository manages. Must derive from BaseEntity.</typeparam>
/// <typeparam name="TId">The type of the entity's identifier (e.g., int, Guid, string).</typeparam>
/// <remarks>
/// This interface is designed to work with specifications (ISpecification<T>) to provide
/// a flexible query mechanism without exposing the underlying ORM (e.g., Entity Framework Core).
/// Implementations should handle all data access concerns including eager loading,
/// filtering, sorting, and pagination.
/// </remarks>
public interface IAsyncRepository<T, TId> where T : BaseEntity<TId>
{
    Task<T> GetByIdAsync(TId id);
    Task<List<T>> ListAllAsync();
    Task<List<T>> ListAsync(ISpecification<T> spec);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<int> CountAsync(ISpecification<T> spec);
}

/// <summary>
/// Generic async repository interface for data access operations on aggregate root entities with integer identity.
/// This is a convenience interface for backward compatibility.
/// </summary>
/// <typeparam name="T">The type of entity this repository manages. Must derive from BaseEntity.</typeparam>
public interface IAsyncRepository<T> : IAsyncRepository<T, int> where T : BaseEntity<int>
{
}