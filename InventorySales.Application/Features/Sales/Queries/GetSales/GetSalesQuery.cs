using InventorySales.Application.Features.Sales.DTOs;

namespace InventorySales.Application.Features.Sales.Queries.GetSales
{
    public record GetSalesQuery() : IRequest<List<SaleDto>>;

}
