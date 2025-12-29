using InventorySales.Domain.DomainEvents.Events;
using InventorySales.Domain.Entities.Auth;
using InventorySales.Domain.Entities.Common;
using InventorySales.Domain.Exceptions;

namespace InventorySales.Domain.Entities
{
    public class Product : AuditableEntity<Guid>
    {
        public string Name { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int CurrentStock { get; private set; }
        public Guid? ModifiedById { get; private set; } 
        public DateTime? ModifiedAt { get; private set; }
        public List<StockMovement> StockMovements { get; private set; } = [];
        public Product()
        {
        }
        public Product(string name, decimal unitPrice, Guid userId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Product name cannot be empty.");
            if (unitPrice < 0)
                throw new DomainException("UnitPrice cannot be negative.");

            Name = name;
            UnitPrice = unitPrice;
            CurrentStock = 0;
            StockMovements = new List<StockMovement>();
            CreatedById = userId;

            AddDomainEvent(new ProductCreatedEvent(this));

        }
        public void UpdatePrice(decimal newPrice, Guid? userId)
        {
            if (newPrice < 0)
                throw new DomainException("UnitPrice cannot be negative.");

            UnitPrice = newPrice;
            ModifiedById = userId;
            ModifiedAt = DateTime.UtcNow;
            AddDomainEvent(new ProductModifiedEvent(this));

        }

        public void IncreaseStock(int quantity)
        {
            if (quantity <= 0)
                throw new DomainException("Quantity must be positive.");

            CurrentStock += quantity;

            StockMovements ??= new List<StockMovement>();
            StockMovements.Add(new StockMovement(Id ,MovementType.Increase, quantity, CreatedById));
            AddDomainEvent(new ProductModifiedEvent(this));

        }

        public void DecreaseStock(int quantity)
        {
            if (quantity <= 0)
                throw new DomainException("Quantity must be positive.");
            if (CurrentStock < quantity)
                throw new DomainException("Insufficient stock.");

            CurrentStock -= quantity;
            StockMovements.Add(new StockMovement(Id, MovementType.Decrease, quantity, CreatedById));
            AddDomainEvent(new ProductModifiedEvent(this));

        }

        public void ApplySale(int quantity)
        {
            DecreaseStock(quantity);
        }
    }
}
