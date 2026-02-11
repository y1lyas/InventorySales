using InventorySales.Application.Abstractions;
using InventorySales.Domain.DomainEvents.Events;
using InventorySales.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Products.Events
{
    public class StockDecreasedEventHandler : INotificationHandler<StockDecreasedEvent>
    {
        public async Task Handle(StockDecreasedEvent notification, CancellationToken ct)
        {
        }
    }
}
