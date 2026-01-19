using InventorySales.Application.Attributes;
using InventorySales.Application.Features.Interfaces;
using InventorySales.Application.Features.Products.DTOs;

namespace InventorySales.Application.Features.Products.Queries.GetProducts
{
    [Cacheable(60, "Products")]
    public sealed record GetAllProductsQuery : IRequest<List<ProductDto>>, IQuery;


}
