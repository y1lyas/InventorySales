using InventorySales.Application.Abstractions.RedisCache;
using InventorySales.Domain.DomainEvents.Events;

namespace InventorySales.Application.Features.Products.Events
{
    public class ProductModifiedEventHandler : INotificationHandler<ProductModifiedEvent>
    {
        private readonly ICacheService _cacheService;

        public ProductModifiedEventHandler(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }
        public async Task Handle(ProductModifiedEvent notification, CancellationToken cancellationToken)
        {
            await _cacheService.RemoveByTagAsync("Products");
        }
    }
}
