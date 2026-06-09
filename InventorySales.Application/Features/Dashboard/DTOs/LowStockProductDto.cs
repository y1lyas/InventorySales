using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Dashboard.DTOs
{
    public class LowStockProductDto
    {
        public Guid ProductId { get; set; }

        public string ProductName { get; set; } = default!;

        public string Sku { get; set; } = default!;

        public int Stock { get; set; }
    }
}
