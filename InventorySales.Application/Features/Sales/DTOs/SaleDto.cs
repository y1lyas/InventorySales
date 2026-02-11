namespace InventorySales.Application.Features.Sales.DTOs
{
    public class SaleDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime SaleDate { get; set; }
        public string? CreatedById { get; set; }
    }
}
