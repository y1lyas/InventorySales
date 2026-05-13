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
    public class StockIncreasedEventHandler : INotificationHandler<StockIncreasedEvent>
    {
        private readonly ICacheService _cacheService;

        public StockIncreasedEventHandler(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task Handle(StockIncreasedEvent notification, CancellationToken ct)
        {
            await _cacheService.RemoveByTagAsync("Products");
            await _cacheService.RemoveByTagAsync("StockMovements");


        }
    }
}
