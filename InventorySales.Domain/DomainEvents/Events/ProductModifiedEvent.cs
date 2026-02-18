using InventorySales.Domain.Entities;
using InventorySales.Domain.ValueObjects;


namespace InventorySales.Domain.DomainEvents.Events
{
    public class ProductPriceUpdateEvent : IDomainEvent
    {
        public Product Product { get; }
        public Money OldPrice { get; }
        public ProductPriceUpdateEvent(Product product, Money oldPrice)
        {
            Product = product;
            OldPrice = oldPrice;
        }
    }

}
