using InventorySales.Application.Attributes;
using InventorySales.Application.Common.Pagination;
using InventorySales.Application.Features.Categories.DTOs;

namespace InventorySales.Application.Features.Categories.Queries.GetAllCategories
{
    [Cacheable(60, "Categories")]
    public record GetAllCategoriesQuery(string? categoryName = null, int PageNumber = 1, int PageSize = 20) : IRequest<PagedResult<CategoryDto>>, IQuery;
}
