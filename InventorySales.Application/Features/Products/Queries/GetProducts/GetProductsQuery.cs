using InventorySales.Application.Features.Products.DTOs;

namespace InventorySales.Application.Features.Products.Queries.GetProducts
{
    public record GetProductsQuery() : IRequest<List<ProductDto>>;


}
