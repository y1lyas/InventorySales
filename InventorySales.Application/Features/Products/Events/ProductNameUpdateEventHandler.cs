using InventorySales.Application.Abstractions.RedisCache;
using InventorySales.Domain.DomainEvents.Events.Product;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Products.Events
{
    public class ProductNameUpdateEventHandler : INotificationHandler<ProductNameUpdateEvent>
    {
        private readonly ICacheService _cacheService;
        private readonly ILogger<ProductPriceUpdateEventHandler> _logger;

        public ProductNameUpdateEventHandler(ICacheService cacheService, ILogger<ProductPriceUpdateEventHandler> logger)
        {
            _cacheService = cacheService;
            _logger = logger;
        }

        public async Task Handle(ProductNameUpdateEvent notification, CancellationToken cancellationToken)
        {
            await _cacheService.RemoveByTagAsync("Products");
            await _cacheService.RemoveByTagAsync("StockMovements");

            _logger.LogInformation("Product name updated for ProductId: {ProductId}, OldName: {OldName}, NewName: {NewName}.", notification.ProductId, notification.OldName, notification.NewName);
        }
    }
}
