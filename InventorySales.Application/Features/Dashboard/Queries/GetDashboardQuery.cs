using InventorySales.Application.Attributes;
using InventorySales.Application.Features.Dashboard.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Dashboard.Queries
{
    public record GetDashboardQuery()
     : IRequest<DashboardDto>, IQuery;
}
