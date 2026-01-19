using InventorySales.Application.Abstractions;
using InventorySales.Application.Abstractions.Repositories;
using InventorySales.Domain.Entities;
using InventorySales.Domain.Entities.Auth;
using InventorySales.Infrastructure.Repositories;

namespace InventorySales.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly InventoryDbContext _context;
        public UnitOfWork(InventoryDbContext context)
        {
            _context = context;
        }
        public IRepository<T> Repository<T>() where T : class
        {
                return new Repository<T>(_context);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => await _context.SaveChangesAsync(cancellationToken);
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
