using InventorySales.Domain.DomainEvents.Events;
using InventorySales.Domain.DomainEvents.Events.Category;
using InventorySales.Domain.DomainEvents.Events.Product;
using InventorySales.Domain.Entities.Common;
using InventorySales.Domain.Entities.Common.Interfaces;
using InventorySales.Domain.Exceptions;
using InventorySales.Domain.ValueObjects;

namespace InventorySales.Domain.Entities
{
    public class Product : AuditableEntity , ISoftDelete
    {
        public string Name { get; private set; }
        public Money Price { get; private set; }
        public Quantity Stock { get; private set; }
        public Sku Sku { get; private set; }
        public Guid? CategoryId { get; private set; }
        public Category Category { get; private set; }

        private readonly List<StockMovement> _stockMovements = new();
        public IReadOnlyCollection<StockMovement> StockMovements => _stockMovements.AsReadOnly();
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? DeletedById { get; set; }
        protected Product() { }
        public Product(string sku, string name, Money unitPrice, Guid? categoryId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Product name cannot be empty.");

            Sku = Sku.Create(sku);
            Name = name;
            Price = unitPrice ?? throw new DomainException("Price is required.");
            Stock = Quantity.From(0);
            CategoryId = categoryId;

            AddDomainEvent(new ProductCreatedEvent(Id, Name, Price));

        }
        public void UpdatePrice(Money newPrice )
        {
            var oldPrice = this.Price;
            Price = newPrice ?? throw new DomainException("New price is required.");
            AddDomainEvent(new ProductPriceUpdateEvent(Id, oldPrice, Price));
        }

        public void IncreaseStock(int amount, string userId)
        {
            Stock = Stock.Add(amount);

            _stockMovements.Add(new StockMovement(Id, MovementType.Increase, amount, userId));

            AddDomainEvent(new StockIncreasedEvent(Id, amount));
        }

        public void DecreaseStock(int amount, string userId)
        {
            Stock = Stock.Subtract(amount);

            _stockMovements.Add(new StockMovement(Id, MovementType.Decrease, amount, userId));

            AddDomainEvent(new StockDecreasedEvent(Id, amount));
        }

        public void AssignCategory(Guid categoryId)
        {
            if (categoryId == Guid.Empty)
                throw new DomainException("Category ID cannot be empty.");

            CategoryId = categoryId;
            AddDomainEvent(new CategoryAssignedEvent());
        }
        public void UnassignCategory()
        {
            if (CategoryId == null) return;

            CategoryId = null;
            AddDomainEvent(new CategoryUnassignedEvent());

        }
        public void SoftDelete(string userId)
        {
            if (IsDeleted) return; 

            IsDeleted = true;
            DeletedAt = DateTime.UtcNow;
            DeletedById = userId;
            AddDomainEvent(new ProductRemovedEvent(Id));
        }
    }
}
