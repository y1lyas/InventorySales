using InventorySales.Application.Attributes;
using InventorySales.Application.Features.Categories.DTOs;

namespace InventorySales.Application.Features.Categories.Queries.GetCategoryById
{
    [Cacheable(60, "Categories")]
    public record GetCategoriesQuery(Guid CategoryId) : IRequest<CategoryDto>, IQuery;
}
