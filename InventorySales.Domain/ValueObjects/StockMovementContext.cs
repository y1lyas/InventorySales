using InventorySales.Domain.Entities;
using InventorySales.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Domain.ValueObjects
{
    public sealed record StockMovementContext(
     string UserId,
     MovementReason Reason,
     Guid? SaleReferenceId = null);
}
