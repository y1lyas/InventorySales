using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Domain.DomainEvents.Events.Category
{
    public class CategoryUnassignedEvent : IDomainEvent
    {
        public Guid ProductId { get; }
        public CategoryUnassignedEvent(Guid productId)
        {
            ProductId = productId;
        }
    }
}
