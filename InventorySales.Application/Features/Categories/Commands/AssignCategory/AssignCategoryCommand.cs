using InventorySales.Application.Features.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Categories.Commands.AssignCategory
{
    public record AssignCategoryCommand(Guid ProductId, Guid CategoryId) : IRequest<Unit>, ICommand;
}
