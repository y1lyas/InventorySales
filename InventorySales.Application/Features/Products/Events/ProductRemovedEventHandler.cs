using InventorySales.Application.Abstractions.RedisCache;
using InventorySales.Domain.DomainEvents.Events.Product;
using Microsoft.Extensions.Logging;

namespace InventorySales.Application.Features.Products.Events
{
    public class ProductRemovedEventHandler : INotificationHandler<ProductRemovedEvent>
    {
        private readonly ICacheService _cacheService;
        private readonly ILogger<ProductRemovedEventHandler> _logger;

        public ProductRemovedEventHandler(ICacheService cacheService, ILogger<ProductRemovedEventHandler> logger)
        {
            _cacheService = cacheService;
            _logger = logger;
        }
        public async Task Handle(ProductRemovedEvent notification, CancellationToken ct)
        {
            await _cacheService.RemoveByTagAsync("Products");
            await _cacheService.RemoveByTagAsync("StockMovements");


            _logger.LogInformation(
               "Product with ID {ProductId} removed.",
               notification.ProductId
           );
        }
    }
}
