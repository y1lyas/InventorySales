using InventorySales.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Domain.ValueObjects
{
    public record Money
    {
        public decimal Amount { get; init; }
        public string Currency { get; init; }

        private Money(decimal amount, string currency = "TRY")
        {
            if (amount < 0) throw new DomainException("Price cannot be negative.");
            Amount = amount;
            Currency = currency;
        }

        public static Money Create(decimal amount, string currency) => new(amount, currency);

        // Operatör aşırı yükleme ile kolay kullanım
        public static Money operator *(int quantity, Money money)
            => new(quantity * money.Amount, money.Currency);
    }
}
