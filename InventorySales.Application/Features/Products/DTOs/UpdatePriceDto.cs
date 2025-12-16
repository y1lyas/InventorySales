namespace InventorySales.Application.Features.Products.DTOs
{
    public class UpdatePriceDto
    {
        public Guid ProductId { get; set; }
        public decimal NewPrice { get; set; }
    }
}
