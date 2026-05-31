namespace InventorySales.Application.Features.Sales.Commands.MakeSale
{
    public record SaleItemRequest(Guid ProductId, int Quantity);
}
