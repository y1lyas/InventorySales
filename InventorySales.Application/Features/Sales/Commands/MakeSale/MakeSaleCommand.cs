using InventorySales.Application.Features.Interfaces;

namespace InventorySales.Application.Features.Sales.Commands.MakeSale
{
    public record MakeSaleCommand(Guid ProductId, int Quantity) : IRequest<Guid>, ICommand;
}
