using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Domain.DomainEvents.Events.Product
{
    public class ProductRemovedEvent : IDomainEvent
    {
        public Guid ProductId { get; }
        public ProductRemovedEvent(Guid productId)
        {
            ProductId = productId;
        }
    }
}
