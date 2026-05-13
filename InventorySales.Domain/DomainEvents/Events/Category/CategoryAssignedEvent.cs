using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Domain.DomainEvents.Events.Category
{
    public class CategoryAssignedEvent : IDomainEvent
    {
        public Guid ProductId { get; }
        public Guid CategoryId { get;}


        public CategoryAssignedEvent(Guid productId, Guid categoryId)
        {
            ProductId = productId;
            CategoryId = categoryId;
        }
    }
}
