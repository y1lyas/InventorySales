using InventorySales.Domain.DomainEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
