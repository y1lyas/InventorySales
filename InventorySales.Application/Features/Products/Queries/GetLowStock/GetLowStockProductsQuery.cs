using InventorySales.Application.Attributes;
using InventorySales.Application.Features.Products.DTOs;

namespace InventorySales.Application.Features.Products.Queries.GetLowStock
{
    [Cacheable(60, "Products")]
    public class GetLowStockProductsQuery(int Threshold = 5) : IRequest<List<ProductDto>>, IQuery
    {
        public int Threshold { get; } = Threshold;
    }

}
