using InventorySales.Application.Attributes;
using InventorySales.Application.Common.Pagination;
using InventorySales.Application.Features.Sales.DTOs;

namespace InventorySales.Application.Features.Sales.Queries.GetSales
{
    [Cacheable(60, "Sales")]
    public sealed record GetAllSalesQuery(decimal? MinAmount,
decimal? MaxAmount,DateTime? StartDate,
    DateTime? EndDate, int PageNumber = 1, int PageSize = 20) : IRequest<PagedResult<SaleDto>>, IQuery;


}
