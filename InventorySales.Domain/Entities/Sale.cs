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
        protected Sale() { }
        public Sale(string currency)
        {
            TotalPrice = Money.Create(0, currency);
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
            var currency = _items.FirstOrDefault()?.UnitPriceAtSale.Currency ?? "TRY";
            TotalPrice = Money.Create(totalAmount, currency);
        }
    }
}
