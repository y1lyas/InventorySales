using InventorySales.Application.Attributes;
using InventorySales.Application.Features.Categories.DTOs;
using InventorySales.Application.Features.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Categories.Queries.GetAllCategories
{
    [Cacheable(60, "Categories")]
    public record GetAllCategoriesQuery(Guid? CategoryId) : IRequest<List<CategoryDto>>, IQuery;
}
