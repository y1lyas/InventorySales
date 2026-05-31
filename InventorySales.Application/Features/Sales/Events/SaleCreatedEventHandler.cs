using InventorySales.Application.Abstractions.RedisCache;
using InventorySales.Application.Features.Products.Events;
using InventorySales.Domain.DomainEvents.Events.Sale;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Sales.Events
{
    public class SaleCreatedEventHandler : INotificationHandler<SaleCreatedEvent>
    {
        private readonly ILogger<ProductCreatedEventHandler> _logger;
        private readonly ICacheService _cacheService;
        public SaleCreatedEventHandler(ILogger<ProductCreatedEventHandler> logger, ICacheService cacheService)
        {
            _logger = logger;
            _cacheService = cacheService;
        }

        public async Task Handle(SaleCreatedEvent notification, CancellationToken ct)
        {

            await _cacheService.RemoveByTagAsync("Sales");

            _logger.LogInformation("Sale created: SaleId: {SaleId}",
                notification.SaleId);

        }
    }
}
