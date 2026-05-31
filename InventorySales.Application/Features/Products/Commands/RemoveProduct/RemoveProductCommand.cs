namespace InventorySales.Application.Features.Products.Commands.RemoveProduct
{
    public record RemoveProductCommand(Guid productId) : IRequest<Unit>, ICommand;
}
