using InventorySales.Application.Attributes;
using InventorySales.Application.Common.Pagination;
using InventorySales.Application.Features.Sales.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Sales.Queries.GetSale
{
    [Cacheable(60, "Sales")]
    public sealed record GetSaleQuery(Guid SaleId) : IRequest<SaleDetailDto>, IQuery;

}
