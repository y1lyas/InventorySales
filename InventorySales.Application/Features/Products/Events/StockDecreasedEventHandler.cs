using InventorySales.Application.Abstractions;
using InventorySales.Application.Abstractions.RedisCache;
using InventorySales.Domain.DomainEvents.Events;
using InventorySales.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
