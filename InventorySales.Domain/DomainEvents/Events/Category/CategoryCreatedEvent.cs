using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Domain.DomainEvents.Events.Category
{
    public class CategoryCreatedEvent : IDomainEvent
    {
        public Guid CategoryId { get; init; }
        public string CategoryName { get; init; }
        public CategoryCreatedEvent(Guid categoryId, string categoryName)
        {
            CategoryId = categoryId;
            CategoryName = categoryName;
        }
    }
}
