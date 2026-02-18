using InventorySales.Domain.Entities.Common;
using InventorySales.Domain.Exceptions;
using InventorySales.Domain.ValueObjects;


namespace InventorySales.Domain.Entities
{
    public class Sale : BaseEntity
    {
        private readonly List<SaleItem> _items = new();
        public IReadOnlyCollection<SaleItem> Items => _items.AsReadOnly();
        public Money TotalPrice { get; private set; }
        public string CreatedById { get; set; }
        public string? ModifiedById { get; set; }
        public DateTime? ModifiedAt { get; set; }

        protected Sale() { }
        public Sale(string userId)
        {
            CreatedById = userId;
            TotalPrice = Money.Create(0, "TL");
        }

        public void AddItem(Product product, int quantity)
        {
            product.DecreaseStock(quantity, CreatedById);

            var item = new SaleItem(product.Id, quantity, product.Price);
            _items.Add(item);

            CalculateTotalPrice();
        }

        private void CalculateTotalPrice()
        {
            var totalAmount = _items.Sum(x => x.LineTotal.Amount);
            var currency = _items.FirstOrDefault()?.UnitPriceAtSale.Currency ?? "TL";
            TotalPrice = Money.Create(totalAmount, currency);
        }
    }
}
