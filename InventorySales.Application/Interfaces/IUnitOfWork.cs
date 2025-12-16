using InventorySales.Domain.Entities;
using InventorySales.Domain.Entities.Auth;

namespace InventorySales.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> Repository<T>() where T : class;
    }
}
