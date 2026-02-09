using InventorySales.Domain.Entities;


namespace InventorySales.Domain.DomainEvents.Events
{
    public class ProductCreatedEvent : IDomainEvent
    {
        public Product Product { get; }
        public ProductCreatedEvent(Product product)
        {
            Product = product;
        }
    }

}
