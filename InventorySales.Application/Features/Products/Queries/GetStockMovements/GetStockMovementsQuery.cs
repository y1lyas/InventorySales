using InventorySales.Application.Attributes;
using InventorySales.Application.Features.Interfaces;
using InventorySales.Application.Features.Products.DTOs;
using InventorySales.Application.Features.Products.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Products.Queries.GetStockMovement
{
    [Cacheable(60, "StockMovements")]
    public sealed record GetStockMovementsQuery(
    Guid ProductId,
    int PageNumber = 1,
    int PageSize = 20) : IRequest<PagedResult<StockMovementDto>>, IQuery;
}
