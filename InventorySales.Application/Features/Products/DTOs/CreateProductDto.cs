namespace InventorySales.Application.Features.Products.DTOs
{
    public class CreateProductDto
    {
        public string Name { get; set; } = null!;
        public decimal UnitPrice { get; set; }
    }
}
