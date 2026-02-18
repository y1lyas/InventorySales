using InventorySales.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Domain.DomainEvents.Events
{
    public class StockDecreasedEvent : IDomainEvent
    {
        public Guid ProductId { get; }
        public int Quantity { get; }
        public string CreatedById { get; }

        public StockDecreasedEvent(Guid productId, int quantity, string createdById)
        {
            ProductId = productId;
            Quantity = quantity;
            CreatedById = createdById;
        }
    }
}
