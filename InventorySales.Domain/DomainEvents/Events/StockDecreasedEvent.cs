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
        public Product Product { get; }

        public StockDecreasedEvent(Product product)
        {
            Product = product;
        }
    }
}
