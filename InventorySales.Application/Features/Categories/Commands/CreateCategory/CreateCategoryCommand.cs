using InventorySales.Application.Features.Categories.DTOs;
using InventorySales.Application.Features.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Categories.Commands.CreateCategory
{
    public record CreateCategoryCommand(string Name, string? Description) : IRequest<CategoryDto>, ICommand;
}
