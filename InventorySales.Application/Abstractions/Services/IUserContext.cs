using InventorySales.Domain.Entities.Auth;

namespace InventorySales.Application.Abstractions.Services
{
    public interface IUserContext
    {
        Task<User> GetCurrentUserAsync(CancellationToken ct);

    }
}
