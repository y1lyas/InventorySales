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
    public class CategoryAssignedEventHandler : INotificationHandler<CategoryAssignedEvent>
    {
        private readonly ICacheService _cacheService;
        private readonly ILogger<CategoryCreatedEventHandler> _logger;


        public CategoryAssignedEventHandler(ICacheService cacheService, ILogger<CategoryCreatedEventHandler> logger)
        {
            _cacheService = cacheService;
            _logger = logger;
        }

        public async Task Handle(CategoryAssignedEvent notification, CancellationToken ct)
        {
            await _cacheService.RemoveByTagAsync("Categories");
            await _cacheService.RemoveByTagAsync("Products");

            _logger.LogInformation("Category assigned: CategoryId: {CategoryId} for ProductId: {ProductId}",
               notification.CategoryId, notification.ProductId);

        }
    }
}
