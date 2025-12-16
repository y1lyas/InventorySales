using InventorySales.Application.Interfaces;
using InventorySales.Infrastructure.Persistence;
using System.Linq.Expressions;

namespace InventorySales.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly InventoryDbContext _context;

        public Repository(InventoryDbContext context) => _context = context;

        private DbSet<T> Table { get => _context.Set<T>(); }

        public IQueryable<T> Query() => Table.AsQueryable();

        public async Task<T> GetByIdAsync(Guid id) =>
            await Table.FindAsync(id);
        public async Task<IEnumerable<T>> GetAllAsync() =>
            await Table.ToListAsync();
        public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await Table.FirstOrDefaultAsync(predicate);
        }
        public async Task AddAsync(T entity) =>
            await Table.AddAsync(entity);

        public async Task UpdateAsync(T entity) =>
            Table.Update(entity);

        public async Task DeleteAsync(T entity) =>
            Table.Remove(entity);

    }
}
