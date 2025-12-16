namespace InventorySales.Application.Features.Products.Commands.UpdatePrice
{
    public record UpdateProductPriceCommand(Guid ProductId, decimal NewPrice) : IRequest<Unit>;
}
