using InventorySales.Domain.Entities.Common;
using InventorySales.Domain.Exceptions;


namespace InventorySales.Domain.Entities
{
    public class Sale : AuditableEntity<Guid>
    {
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal TotalPrice { get; private set; }
        public DateTime SaleDate { get; private set; }
        protected Sale() { }
        public Sale(Guid productId, int quantity, decimal unitPrice, Guid userId)
        {
            if (quantity <= 0)
                throw new DomainException("Quantity must be positive.");

            ProductId = productId;
            Quantity = quantity;
            TotalPrice = quantity * unitPrice;
            SaleDate = DateTime.UtcNow;
            CreatedById = userId;
        }
    }
}
