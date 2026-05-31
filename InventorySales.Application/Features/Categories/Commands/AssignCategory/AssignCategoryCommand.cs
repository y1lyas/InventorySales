namespace InventorySales.Application.Features.Categories.Commands.AssignCategory
{
    public record AssignCategoryCommand(Guid ProductId, Guid CategoryId) : IRequest<Unit>, ICommand;
}
