using InventorySales.Application.Attributes;
using InventorySales.Application.Features.Interfaces;
using InventorySales.Application.Features.Products.DTOs;
using InventorySales.Application.Features.Products.Paging;

namespace InventorySales.Application.Features.Products.Queries.GetProducts
{
    [Cacheable(60, "Products")]
    public sealed record GetAllProductsQuery(
            Guid? CategoryId,
            string? SearchTerm,
            bool? IsDeleted,
            int PageNumber = 1,
            int PageSize = 20 ) : IRequest<PagedResult<ProductDto>>, IQuery;


}
