using InventorySales.Application.Attributes;
using InventorySales.Application.Common.Pagination;
using InventorySales.Application.Features.Products.DTOs;

namespace InventorySales.Application.Features.Products.Queries.GetStockMovement
{
    [Cacheable(60, "StockMovements")]
    public sealed record GetStockMovementsQuery(
    Guid ProductId,
    int PageNumber = 1,
    int PageSize = 20) : IRequest<PagedResult<StockMovementDto>>, IQuery;
}
