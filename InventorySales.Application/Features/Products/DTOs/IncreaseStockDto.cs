namespace InventorySales.Application.Features.Products.DTOs
{
    public class IncreaseStockDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
