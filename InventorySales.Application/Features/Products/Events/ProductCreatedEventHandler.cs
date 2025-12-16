using InventorySales.Domain.DomainEvents.Events;

namespace InventorySales.Application.Features.Products.Events
{
    public class ProductCreatedEventHandler : INotificationHandler<ProductCreatedEvent>
    {
        public Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Product created successfully: Product Name : {notification.Product.Name} | Unit Price {notification.Product.UnitPrice} ");
            return Task.CompletedTask;
        }
    }
}
