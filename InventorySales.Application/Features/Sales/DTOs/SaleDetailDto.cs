using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Sales.DTOs
{
    public class SaleDetailDto
    {
        public Guid Id { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime SaleDate { get; set; }
        public List<SaleItemDto> Items { get; set; } = [];
    }
}
