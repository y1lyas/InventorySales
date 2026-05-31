using InventorySales.Application.Attributes;
using InventorySales.Application.Common.Pagination;
using InventorySales.Application.Features.Products.DTOs;
using InventorySales.Domain.Entities;
using InventorySales.Domain.Enums;

namespace InventorySales.Application.Features.Products.Queries.GetAllStockMovements
{
    [Cacheable(60, "StockMovements")]
    public sealed record GetAllStockMovementsQuery(
 Guid? ProductId,
 string? SearchTerm,
 MovementType? MovementType,
 MovementReason? MovementReason,
 DateTime? StartDate,
 DateTime? EndDate,
  int? MinQuantity,
 int? MaxQuantity,
 int PageNumber = 1,
 int PageSize = 20) : IRequest<PagedResult<StockMovementDto>>, IQuery;
}
