using InventorySales.Application.Attributes;
using InventorySales.Application.Features.Categories.DTOs;
using InventorySales.Application.Features.Interfaces;
using InventorySales.Application.Features.Products.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Categories.Queries.GetAllCategories
{
    [Cacheable(60, "Categories")]
    public record GetAllCategoriesQuery(string? categoryName = null, int PageNumber = 1, int PageSize = 20) : IRequest<PagedResult<CategoryDto>>, IQuery;
}
