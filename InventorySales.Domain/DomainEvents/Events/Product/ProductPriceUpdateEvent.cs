using InventorySales.Domain.Entities;
using InventorySales.Domain.ValueObjects;


namespace InventorySales.Domain.DomainEvents.Events.Product
{
    public class ProductPriceUpdateEvent : IDomainEvent
    {
        public Guid ProductId { get; }
        public Money OldPrice { get; }
        public Money NewPrice { get; }

        public ProductPriceUpdateEvent(Guid productId, Money oldPrice, Money newPrice)
        {
            ProductId = productId;
            OldPrice = oldPrice;
            NewPrice = newPrice;
        }
    }

}
