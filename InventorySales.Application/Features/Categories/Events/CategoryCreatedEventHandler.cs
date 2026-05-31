using InventorySales.Application.Abstractions.RedisCache;
using InventorySales.Domain.DomainEvents.Events.Category;
using Microsoft.Extensions.Logging;

namespace InventorySales.Application.Features.Categories.Events
{
    public class CategoryCreatedEventHandler : INotificationHandler<CategoryCreatedEvent>
    {
        private readonly ICacheService _cacheService;
        private readonly ILogger<CategoryCreatedEventHandler> _logger;
        public CategoryCreatedEventHandler(ICacheService cacheService, ILogger<CategoryCreatedEventHandler> logger)
        {
            _cacheService = cacheService;
            _logger = logger;
        }
        public async Task Handle(CategoryCreatedEvent notification, CancellationToken ct)
        {
            await _cacheService.RemoveByTagAsync("Categories");

            _logger.LogInformation("Category created: CategoryId: {CategoryId} | CategoryName: {CategoryName}",
                notification.CategoryId, notification.CategoryName);
        }
    }
}
