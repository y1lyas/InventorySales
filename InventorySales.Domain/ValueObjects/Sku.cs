using InventorySales.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InventorySales.Domain.ValueObjects
{
    public record Sku
    {
        public string Value { get; init; }

        private Sku() { }

        private Sku(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("SKU boş olamaz.");

            var processed = value.Trim().ToUpperInvariant();

            processed = Regex.Replace(processed, @"\s+", "-");

            if (!Regex.IsMatch(processed, @"^[A-Z0-9-]+$"))
                throw new DomainException("SKU geçersiz karakter içeriyor.");

            //  Ardışık tireleri teke indir (Örn: "ABC---123" -> "ABC-123")
            processed = Regex.Replace(processed, @"-+", "-");

            
            processed = processed.Trim('-');

            if (processed.Length < 3)
                throw new DomainException("SKU 3 karakterden fazla olmalı.");

            Value = processed;
        }
        public static Sku Create(string value) => new(value);
    }
}
