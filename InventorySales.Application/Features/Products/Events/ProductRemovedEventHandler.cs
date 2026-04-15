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

            _logger.LogInformation(
               "Product with ID {ProductId} removed.",
               notification.ProductId
           );
        }
    }
}
