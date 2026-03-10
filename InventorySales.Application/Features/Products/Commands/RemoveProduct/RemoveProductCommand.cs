using InventorySales.Application.Features.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Products.Commands.RemoveProduct
{
   public record RemoveProductCommand(Guid productId) : IRequest<Unit>, ICommand;
}
