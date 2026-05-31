using InventorySales.Domain.Entities.Common;
using InventorySales.Domain.Enums;
using InventorySales.Domain.Exceptions;


namespace InventorySales.Domain.Entities
{
    public class StockMovement : BaseEntity
    {
        public Guid ProductId { get; private set; }
        public Product Product { get; private set; }
        public int Quantity { get; private set; }
        public MovementType MovementType { get; private set; }
        public MovementReason Reason { get; private set; }
        protected StockMovement() { }
        public Guid? SaleReferenceId { get; private set; }

        public StockMovement(Guid productId, MovementType type, MovementReason reason, int quantity ,string userId, Guid? saleReferenceId = null)
        {
            if (quantity <= 0)
                throw new DomainException("Quantity must be positive.");

            ProductId = productId;
            Quantity = quantity;
            MovementType = type;
            Reason = reason;
            SaleReferenceId = saleReferenceId;
            CreatedById = userId;
        }
    }
}
