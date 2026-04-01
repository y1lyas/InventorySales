using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Products.Paging
{
    public sealed record PagedResult<T>(
     List<T> Items,
     int PageNumber,
     int PageSize,
     long TotalCount,
     int TotalPages);
}
