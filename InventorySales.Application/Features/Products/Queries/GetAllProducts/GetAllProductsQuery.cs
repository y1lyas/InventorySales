using InventorySales.Application.Attributes;
using InventorySales.Application.Common.Pagination;
using InventorySales.Application.Features.Products.DTOs;

namespace InventorySales.Application.Features.Products.Queries.GetProducts
{
    [Cacheable(60, "Products")]
    public sealed record GetAllProductsQuery(
            Guid? CategoryId,
            string? SearchTerm,
            bool? IsDeleted,
            int? MinStock,
            int? MaxStock,
            decimal? MinPrice,
            decimal? MaxPrice,
            DateTime? StartDate,
            DateTime? EndDate,
            int PageNumber = 1,
            int PageSize = 20) : IRequest<PagedResult<ProductDto>>, IQuery;


}
