using System.Linq.Expressions;

namespace InventorySales.Application.Abstractions.Repositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Query();
        Task<T> GetByIdAsync(Guid id);
        Task<T?> GetAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
