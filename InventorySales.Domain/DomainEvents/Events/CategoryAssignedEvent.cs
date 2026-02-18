using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Domain.DomainEvents.Events
{
    public class CategoryAssignedEvent : IDomainEvent
    {
        public Guid CategoryId { get; }
        public CategoryAssignedEvent(Guid categoryId)
        {
            CategoryId = categoryId;
        }
    }
}
