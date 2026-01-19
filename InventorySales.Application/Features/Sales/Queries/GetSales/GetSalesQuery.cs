using InventorySales.Application.Attributes;
using InventorySales.Application.Features.Interfaces;
using InventorySales.Application.Features.Sales.DTOs;

namespace InventorySales.Application.Features.Sales.Queries.GetSales
{
    [Cacheable(60, "Sales")]
    public sealed record GetSalesQuery() : IRequest<List<SaleDto>>, IQuery;


}
