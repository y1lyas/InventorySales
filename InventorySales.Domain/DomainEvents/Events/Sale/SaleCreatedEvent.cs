using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Domain.DomainEvents.Events.Sale
{
    public class SaleCreatedEvent : IDomainEvent
    {
        public Guid SaleId { get; }

        public SaleCreatedEvent(Guid saleId)
        {
            SaleId = saleId;
        }
    }
}
