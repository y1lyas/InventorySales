using InventorySales.Domain.Entities.Common;
using InventorySales.Domain.Exceptions;


namespace InventorySales.Domain.Entities
{
    public class Sale : BaseEntity, IAuditableEntity
    {
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal TotalPrice { get; private set; }
        public string CreatedById { get; set; }
        public string? ModifiedById { get; set; }
        public DateTime? ModifiedAt { get; set; }

        protected Sale() { }
        public Sale(Guid productId, int quantity, decimal unitPrice, string userId)
        {
            if (quantity <= 0)
                throw new DomainException("Quantity must be positive.");

            ProductId = productId;
            Quantity = quantity;
            TotalPrice = quantity * unitPrice;
            CreatedById = userId;

        }
    }
}
