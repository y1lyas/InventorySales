using InventorySales.Application.Abstractions.RedisCache;
using InventorySales.Domain.DomainEvents.Events.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Categories.Events
{
    public class CategoryAssignedEventHandler : INotificationHandler<CategoryAssignedEvent>
    {
        private readonly ICacheService _cacheService;

        public CategoryAssignedEventHandler(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task Handle(CategoryAssignedEvent notification, CancellationToken ct)
        {
            await _cacheService.RemoveByTagAsync("Categories");

        }
    }
}
