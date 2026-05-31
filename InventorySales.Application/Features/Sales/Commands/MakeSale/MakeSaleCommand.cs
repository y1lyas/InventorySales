namespace InventorySales.Application.Features.Sales.Commands.MakeSale
{
    public record MakeSaleCommand(List<SaleItemRequest> Items) : IRequest<Guid>, ICommand;
}
