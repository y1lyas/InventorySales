using InventorySales.Domain.Entities;

namespace InventorySales.Application.Features.Products.DTOs
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public string? Currency { get; set; }
        public int CurrentStock { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string? CreatedById { get; set; }
        public string? SKUnit { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
