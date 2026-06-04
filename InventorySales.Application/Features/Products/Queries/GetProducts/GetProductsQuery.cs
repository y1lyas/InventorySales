using InventorySales.Application.Attributes;
using InventorySales.Application.Features.Products.DTOs;

namespace InventorySales.Application.Features.Products.Queries.GetProductById
{
    [Cacheable(60, "Products")]
    public sealed record GetProductsQuery(Guid ProductId) : IRequest<ProductDto>, IQuery;
}
