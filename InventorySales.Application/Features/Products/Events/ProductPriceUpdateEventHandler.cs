using InventorySales.Application.Abstractions.RedisCache;
using InventorySales.Domain.DomainEvents.Events;
using Microsoft.Extensions.Logging;

namespace InventorySales.Application.Features.Products.Events
{
    public class ProductPriceUpdateEventHandler : INotificationHandler<ProductPriceUpdateEvent>
    {
        private readonly ICacheService _cacheService;
        private readonly ILogger<ProductPriceUpdateEventHandler> _logger;

        public ProductPriceUpdateEventHandler(ICacheService cacheService, ILogger<ProductPriceUpdateEventHandler> logger)
        {
            _cacheService = cacheService;
            _logger = logger;
        }

        public async Task Handle(ProductPriceUpdateEvent notification, CancellationToken cancellationToken)
        {
            await _cacheService.RemoveByTagAsync("Products");
            _logger.LogInformation(
               "Product price updated for {ProductId} OldPrice: {@OldPrice}, NewPrice: {@NewPrice}",
               notification.Product.Id,
               notification.OldPrice,
               notification.Product.Price
            );



        }
    }
}
