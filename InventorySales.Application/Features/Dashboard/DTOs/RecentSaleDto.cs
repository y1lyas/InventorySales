using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Dashboard.DTOs
{
    public class RecentSaleDto
    {
        public Guid Id { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime SaleDate { get; set; }
    }
}
