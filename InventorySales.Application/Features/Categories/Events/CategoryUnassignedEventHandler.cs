using InventorySales.Application.Abstractions.RedisCache;
using InventorySales.Domain.DomainEvents.Events.Category;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<CategoryCreatedEventHandler> _logger;

        public CategoryUnassignedEventHandler(ICacheService cacheService, ILogger<CategoryCreatedEventHandler> logger)
        {
            _cacheService = cacheService;
            _logger = logger;
        }
        public async Task Handle(CategoryUnassignedEvent notification, CancellationToken ct)
        {
            await _cacheService.RemoveByTagAsync("Categories");
            await _cacheService.RemoveByTagAsync("Products");


            _logger.LogInformation("Category Unassigned for ProductId: {ProductId}",
              notification.ProductId);
        }
    }
}
