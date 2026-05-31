using InventorySales.Application.Features.Products.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Products.Commands.UpdateName
{
    public sealed record UpdateProductNameCommand(
     Guid ProductId,
     string Name
 ) : IRequest<Unit>, ICommand;
}
