using InventorySales.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Dashboard.DTOs
{
    public class RecentStockMovementDto
    {
        public Guid ProductId { get; set; }

        public string ProductName { get; set; } = default!;

        public int Quantity { get; set; }
        public MovementType MovementType { get; set; }

        public MovementReason Reason { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
