using InventorySales.Domain.Entities;
using InventorySales.Domain.ValueObjects;


namespace InventorySales.Domain.DomainEvents.Events.Product
{
    public class ProductCreatedEvent : IDomainEvent
    {
        public Guid ProductId { get; }
        public string Name { get; }
        public Money Price { get; }

        public ProductCreatedEvent(Guid productId, string name, Money price)
        {
            ProductId = productId;
            Name = name;
            Price = price;
        }
    }

}
