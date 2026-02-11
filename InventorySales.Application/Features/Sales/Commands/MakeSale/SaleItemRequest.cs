using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Sales.Commands.MakeSale
{
    public record SaleItemRequest(Guid ProductId, int Quantity);
}
