namespace InventorySales.Application.Features.Products.DTOs
{
    public class DecreaseStockDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
