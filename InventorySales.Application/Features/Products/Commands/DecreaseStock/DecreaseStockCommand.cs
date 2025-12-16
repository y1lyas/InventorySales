namespace InventorySales.Application.Features.Products.Commands.DecreaseStock
{
    public record DecreaseStockCommand(Guid ProductId, int Quantity) : IRequest<Unit>;

}
