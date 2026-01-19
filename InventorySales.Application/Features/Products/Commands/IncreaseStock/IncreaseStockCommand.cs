using InventorySales.Application.Features.Interfaces;

namespace InventorySales.Application.Features.Products.Commands.IncreaseStock
{
    public record IncreaseStockCommand(Guid ProductId, int Quantity) : IRequest<Unit>, ICommand;
}
