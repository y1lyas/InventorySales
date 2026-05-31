namespace InventorySales.Application.Features.Categories.Commands.UnassignCategory
{
    public record UnassignCategoryCommand(Guid ProductId) : IRequest<Unit>, ICommand;
}
