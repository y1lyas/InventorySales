using InventorySales.Application.Interfaces;
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

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
