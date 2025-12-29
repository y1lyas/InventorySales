using InventorySales.Application.Abstractions.RedisCache;
using InventorySales.Domain.DomainEvents.Events;

namespace InventorySales.Application.Features.Products.Events
{
    public class ProductCreatedEventHandler : INotificationHandler<ProductCreatedEvent>
    {
        private readonly ICacheService _cacheService;

        public ProductCreatedEventHandler(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }
        public async Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
        {
            await _cacheService.RemoveAsync("GetAllProducts");

            // Eğer ürüne özel cache anahtarı kullanıyorsanız; örnek:
            //if (notification.Product.Id != null)
            //    await _cacheService.RemoveAsync($"Product:{notification.Product.Id}");

            Console.WriteLine($"Product created successfully: Product Name : {notification.Product.Name} | Unit Price {notification.Product.UnitPrice} ");
        }
    }
}
