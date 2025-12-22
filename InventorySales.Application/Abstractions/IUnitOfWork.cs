using InventorySales.Application.Abstractions.Repositories;

namespace InventorySales.Application.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> Repository<T>() where T : class;
    }
}
