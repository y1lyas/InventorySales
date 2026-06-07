using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Dashboard.DTOs
{
    public class DashboardDto
    {
        public int TotalProducts { get; set; }

        public int TotalCategories { get; set; }

        public int LowStockProducts { get; set; }

        public decimal TodaySalesAmount { get; set; }

        public int TodaySalesCount { get; set; }

        public List<RecentSaleDto> RecentSales { get; set; } = [];

        public List<RecentStockMovementDto> RecentMovements { get; set; } = [];
    }
}
