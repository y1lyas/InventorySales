using InventorySales.Domain.DomainEvents.Events;
using InventorySales.Domain.Entities.Common;
using InventorySales.Domain.Exceptions;
using InventorySales.Domain.ValueObjects;

namespace InventorySales.Domain.Entities
{
    public class Product : BaseEntity, IAuditableEntity
    {
        public string Name { get; private set; }
        public Money Price { get; private set; }
        public Quantity Stock { get; private set; }
        public Sku Sku { get; private set; }
        public Guid? CategoryId { get; set; }
        public Category Category { get; set; } = new Category();

        private readonly List<StockMovement> _stockMovements = new();
        public IReadOnlyCollection<StockMovement> StockMovements => _stockMovements.AsReadOnly(); public string CreatedById { get; set; }
        public string? ModifiedById { get; set; }
        public DateTime? ModifiedAt { get; set; }

        public Product()
        {
        }
        public Product(string sku, string name, Money unitPrice, string userId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Product name cannot be empty.");

            Sku = Sku.Create(sku);
            Name = name;
            Price = unitPrice ?? throw new DomainException("Price is required.");
            Stock = Quantity.From(0);
            CreatedById = userId;

            AddDomainEvent(new ProductCreatedEvent(this));

        }
        public void UpdatePrice(Money newPrice, string? userId)
        {
            var oldPrice = this.Price;
            Price = newPrice ?? throw new DomainException("New price is required.");
            ModifiedById = userId;
            ModifiedAt = DateTime.UtcNow;
            AddDomainEvent(new ProductPriceUpdateEvent(this, oldPrice));
        }

        public void IncreaseStock(int amount, string userId)
        {
            Stock = Stock.Add(amount);

            _stockMovements.Add(new StockMovement(Id, MovementType.Increase, amount, userId));

            AddDomainEvent(new StockIncreasedEvent(this));
        }

        public void DecreaseStock(int amount, string userId)
        {
            Stock = Stock.Subtract(amount);

            _stockMovements.Add(new StockMovement(Id, MovementType.Decrease, amount, userId));

            AddDomainEvent(new StockDecreasedEvent(this));
        }
    }
}
