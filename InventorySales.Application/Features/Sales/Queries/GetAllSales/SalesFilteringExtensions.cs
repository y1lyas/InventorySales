using InventorySales.Application.Common.Extensions;
using InventorySales.Application.Features.Sales.Queries.GetSales;
using InventorySales.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Sales.Queries.GetAllSales
{
    public static class SalesFilteringExtensions
    {
        public static IQueryable<Sale> ApplyFilters(
            this IQueryable<Sale> query,
            GetAllSalesQuery request)
        {
            return query
                .WhereIf(
                    request.StartDate.HasValue,
                    x => x.CreatedDate >= request.StartDate!.Value)

                .WhereIf(
                    request.EndDate.HasValue,
                    x => x.CreatedDate < request.EndDate!.Value.Date.AddDays(1))

                .WhereIf(
                    request.MinAmount.HasValue,
                    x => x.TotalPrice.Amount >= request.MinAmount!.Value)

                .WhereIf(
                    request.MaxAmount.HasValue,
                    x => x.TotalPrice.Amount <= request.MaxAmount!.Value);
        }
    }
}
