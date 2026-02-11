using InventorySales.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Domain.ValueObjects
{
    public record Quantity
    {
        public int Value { get; init; }

        protected Quantity() { }

        private Quantity(int value)
        {
            if (value < 0) throw new DomainException("Quantity cannot be negative.");
            Value = value;
        }

        public static Quantity From(int value) => new(value);

        public Quantity Add(int amount) => new(Value + amount);
        public Quantity Subtract(int amount)
        {
            if (Value < amount) throw new DomainException("Insufficient stock.");
            return new(Value - amount);
        }
    }
}
