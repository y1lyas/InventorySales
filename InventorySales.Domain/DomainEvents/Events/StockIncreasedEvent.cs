using InventorySales.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Domain.DomainEvents.Events
{
    public class StockIncreasedEvent : IDomainEvent
    {
        public Product Product { get; }

        public StockIncreasedEvent(Product product)
        {
            Product = product;
        }
    }
}
