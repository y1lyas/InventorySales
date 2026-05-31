using InventorySales.Application.Abstractions.RedisCache;
using InventorySales.Domain.DomainEvents.Events;

namespace InventorySales.Application.Features.Products.Events
{
    public class StockDecreasedEventHandler : INotificationHandler<StockDecreasedEvent>
    {
        private readonly ICacheService _cacheService;

        public StockDecreasedEventHandler(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task Handle(StockDecreasedEvent notification, CancellationToken ct)
        {
            await _cacheService.RemoveByTagAsync("Products");
            await _cacheService.RemoveByTagAsync("StockMovements");

        }
    }
}
