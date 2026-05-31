using InventorySales.Domain.Entities;
using InventorySales.Domain.Enums;

namespace InventorySales.Application.Features.Products.DTOs
{
    public class StockMovementDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductSku { get; set; }
        public int Quantity { get; set; }
        public MovementType MovementType { get; set; }
        public MovementReason Reason { get; set; }
        public Guid? SaleReferenceId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedById { get; set; }
    }
}
