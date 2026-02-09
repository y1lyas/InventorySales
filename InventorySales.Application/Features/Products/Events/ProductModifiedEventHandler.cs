using InventorySales.Application.Abstractions.RedisCache;
using InventorySales.Domain.DomainEvents.Events;
using Microsoft.Extensions.Logging;

namespace InventorySales.Application.Features.Products.Events
{
    public class ProductModifiedEventHandler : INotificationHandler<ProductModifiedEvent>
    {
        private readonly ICacheService _cacheService;
        private readonly ILogger<ProductModifiedEventHandler> _logger;

        public ProductModifiedEventHandler(ICacheService cacheService, ILogger<ProductModifiedEventHandler> logger)
        {
            _cacheService = cacheService;
            _logger = logger;
        }

        public async Task Handle(ProductModifiedEvent notification, CancellationToken cancellationToken)
        {
            await _cacheService.RemoveByTagAsync("Products");
            _logger.LogInformation($"Product modified. Product Name : {notification.Product.Name} | Unit Price : {notification.Product.UnitPrice}");
        }
    }
}
