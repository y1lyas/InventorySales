using InventorySales.Domain.Entities.Common;
using InventorySales.Domain.Exceptions;


namespace InventorySales.Domain.Entities
{
    public enum MovementType
    {
        Increase = 0,
        Decrease = 1
    }
    public class StockMovement : BaseEntity
    {
        public Guid ProductId { get; private set; }
        public Product Product { get; private set; }
        public int Quantity { get; private set; }
        public MovementType MovementType { get; private set; }
        public string CreatedById { get; set; }
        protected StockMovement() { }

        public StockMovement(Guid productId, MovementType type, int quantity, string userId)
        {
            if (quantity <= 0)
                throw new DomainException("Quantity must be positive.");

            ProductId = productId;
            Quantity = quantity;
            MovementType = type;
            CreatedById = userId;
        }
    }
}
