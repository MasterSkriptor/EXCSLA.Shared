using System.Collections.Generic;
using System.Threading.Tasks;
using EXCSLA.Shared.Core;

namespace Core.Interfaces
{
    public interface IAsyncRepository<T> where T : BaseEntity<int>
    {
        Task<T> GetByIdAsync(int id);
        Task<List<T>> ListAllAsync();
        Task<List<T>> ListAsync(ISpecification<T> spec);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<int> CountAsync(ISpecification<T> spec);
    }
}