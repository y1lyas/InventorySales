using InventorySales.Domain.Entities;
using InventorySales.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;


namespace InventorySales.Tests.Domain
{
    public class ProductTests
    {
        [Fact]
        public void Create_ShouldCreateProduct_WhenInputsAreValid()
        {
            var product = new Product(
              name: "Test Product",
              unitPrice: 100,
              userId: "user-123"
               );

            Assert.Equal("Test Product", product.Name);
            Assert.Equal("user-123", product.CreatedById);
            Assert.NotEmpty(product.DomainEvents);
        }
        [Fact]
        public void Constructor_ShouldThrow_WhenNameIsEmpty()
        {
            Assert.Throws<DomainException>(() =>
            {
                new Product("", 100, "user-123");
            });
        }
        [Fact]
        public void Constructor_ShouldThrow_WhenUnitPriceIsNegative()
        {
            Assert.Throws<DomainException>(() =>
            {
                new Product("Test", -10, "user-123");
            });
        }


    }
}
