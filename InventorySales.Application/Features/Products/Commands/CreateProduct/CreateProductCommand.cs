namespace InventorySales.Application.Features.Products.Commands.CreateProduct
{
    public record CreateProductCommand(string Name, decimal UnitPrice) : IRequest<Guid>;

}
