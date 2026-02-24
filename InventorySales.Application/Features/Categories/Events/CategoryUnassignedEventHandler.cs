using InventorySales.Application.Abstractions.RedisCache;
using InventorySales.Domain.DomainEvents.Events.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Categories.Events
{
    public class CategoryUnassignedEventHandler : INotificationHandler<CategoryUnassignedEvent>
    {
        private readonly ICacheService _cacheService;
        public CategoryUnassignedEventHandler(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }
        public async Task Handle(CategoryUnassignedEvent notification, CancellationToken ct)
        {
            await _cacheService.RemoveByTagAsync("Categories");
        }
    }
}
