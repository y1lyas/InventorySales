using InventorySales.Application.Features.Products.DTOs;

namespace InventorySales.Application.Features.Products.Queries.GetLowStock
{
    public record GetLowStockProductsQuery(int Threshold = 5) : IRequest<List<ProductDto>>;

}
