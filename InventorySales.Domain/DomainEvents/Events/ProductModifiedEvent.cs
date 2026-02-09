using InventorySales.Domain.Entities;


namespace InventorySales.Domain.DomainEvents.Events
{
    public class ProductModifiedEvent : IDomainEvent
    {
        public Product Product { get; }
        public ProductModifiedEvent(Product product)
        {
            Product = product;
        }
    }

}
