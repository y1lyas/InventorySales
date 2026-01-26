using InventorySales.Application.Abstractions.RedisCache;
using InventorySales.Domain.DomainEvents.Events;
using Microsoft.Extensions.Logging;

namespace InventorySales.Application.Features.Products.Events
{
    public class ProductCreatedEventHandler : INotificationHandler<ProductCreatedEvent>
    {
        private readonly ICacheService _cacheService;
        private readonly ILogger <ProductCreatedEventHandler> _logger;

        public ProductCreatedEventHandler(ICacheService cacheService, ILogger<ProductCreatedEventHandler> logger)
        {
            _cacheService = cacheService;
            _logger = logger;
        }
        public async Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
        {
            await _cacheService.RemoveByTagAsync("Products");

            _logger.LogInformation($"Product created successfully: Product Name : {notification.Product.Name} | Unit Price {notification.Product.UnitPrice}");
        }
    }
}
