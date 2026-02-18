using InventorySales.Application.Features.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Categories.Commands.UnassignCategory
{
    public record UnassignCategoryCommand(Guid ProductId) : IRequest<Unit>, ICommand; 
}
