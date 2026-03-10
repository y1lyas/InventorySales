using InventorySales.Domain.DomainEvents.Events.Category;
using InventorySales.Domain.Entities.Common;
using InventorySales.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        protected Category() { }
        public Category(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Category name cannot be empty.");

            Name = name;
            Description = description;

            AddDomainEvent(new CategoryCreatedEvent(Id, name));
        }
        public ICollection<Product> Products { get; set; }
    }
}
