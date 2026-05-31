using InventorySales.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Domain.DomainEvents.Events.Product
{
    public class ProductNameUpdateEvent : IDomainEvent
    {
        public Guid ProductId { get; }
        public string OldName { get; }
        public string NewName { get; }

        public ProductNameUpdateEvent(Guid productId, string oldName, string newName)
        {
            ProductId = productId;
            OldName = oldName;
            NewName = newName;
        }
    }
}
