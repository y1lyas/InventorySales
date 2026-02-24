using InventorySales.Application.Abstractions.RedisCache;
using InventorySales.Application.Abstractions.Services;
using InventorySales.Domain.DomainEvents.Events.Product;
using InventorySales.Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace InventorySales.Application.Features.Products.Events
{
    public class ProductCreatedEventHandler : INotificationHandler<ProductCreatedEvent>
    {
        private readonly ILogger<ProductCreatedEventHandler> _logger;
        private readonly ICacheService _cacheService;

        public ProductCreatedEventHandler(ILogger<ProductCreatedEventHandler> logger, ICacheService cacheService)
        {
            _logger = logger;
            _cacheService = cacheService;
        }

        public async Task Handle(ProductCreatedEvent notification, CancellationToken ct)
        {
            await _cacheService.RemoveByTagAsync("Products");

            _logger.LogInformation("Product created: ProductId: {ProductId} | Name: {Name} | Unit Price: {@Price}",
                notification.ProductId, notification.Name, notification.Price);
        }
    }
}
