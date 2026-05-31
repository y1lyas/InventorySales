using InventorySales.Application.Features.Categories.DTOs;

namespace InventorySales.Application.Features.Categories.Commands.CreateCategory
{
    public record CreateCategoryCommand(string Name, string? Description) : IRequest<CategoryDto>, ICommand;
}
