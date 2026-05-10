using InventorySales.Application.Attributes;
using InventorySales.Application.Features.Interfaces;
using InventorySales.Application.Features.Products.DTOs;
using InventorySales.Application.Features.Products.Paging;
using InventorySales.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Products.Queries.GetAllStockMovements
{
    [Cacheable(60, "StockMovements")]
    public sealed record GetAllStockMovementsQuery(
 Guid? ProductId,
 string? SearchTerm,
 MovementType? MovementType,
 int PageNumber = 1,
 int PageSize = 20) : IRequest<PagedResult<StockMovementDto>>, IQuery;
}
