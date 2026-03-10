using InventorySales.Domain.Entities.Common;
using InventorySales.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Domain.Entities
{
    public class SaleItem : BaseEntity
    {
        public Guid ProductId { get; private set; }
        public Quantity Quantity { get; private set; }
        public Money UnitPriceAtSale { get; private set; } 
        public Money LineTotal => Money.Create(Quantity.Value * UnitPriceAtSale.Amount, UnitPriceAtSale.Currency);
        private SaleItem() { } 

        public SaleItem(Guid productId, int quantity, Money unitPrice)
        {
            ProductId = productId;
            Quantity = Quantity.From(quantity);
            UnitPriceAtSale = unitPrice;
        }
    }
}
