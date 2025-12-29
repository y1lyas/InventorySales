using InventorySales.Application.Abstractions.RedisCache;
using InventorySales.Domain.DomainEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Products.Events
{
    public class ProductModifiedEventHandler : INotificationHandler<ProductModifiedEvent>
    {
        private readonly ICacheService _cacheService;

        public ProductModifiedEventHandler(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }
        public async Task Handle(ProductModifiedEvent notification, CancellationToken cancellationToken)
        {
            await _cacheService.RemoveAsync("GetAllProducts");
        }
    }
}
