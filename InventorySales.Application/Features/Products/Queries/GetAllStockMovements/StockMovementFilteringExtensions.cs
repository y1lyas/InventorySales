using InventorySales.Application.Common.Extensions;
using InventorySales.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Products.Queries.GetAllStockMovements
{
    public static class StockMovementFilteringExtensions
    {
        public static IQueryable<StockMovement> ApplyFilters(
            this IQueryable<StockMovement> query,
            GetAllStockMovementsQuery request)
        {
            var search = request.SearchTerm?.Trim().ToLower();

            return query
                .WhereIf(
                    request.ProductId.HasValue,
                    x => x.ProductId == request.ProductId!.Value)

                .WhereIf(
                    request.MovementType.HasValue,
                    x => x.MovementType == request.MovementType!.Value)

                .WhereIf(
                    request.MovementReason.HasValue,
                    x => x.Reason == request.MovementReason!.Value)

                .WhereIf(
                    request.StartDate.HasValue,
                    x => x.CreatedDate >= request.StartDate!.Value)

                .WhereIf(
                    request.EndDate.HasValue,
                    x => x.CreatedDate < request.EndDate!.Value.Date.AddDays(1))

                .WhereIf(
                    request.MinQuantity.HasValue,
                    x => x.Quantity >= request.MinQuantity!.Value)

                .WhereIf(
                    request.MaxQuantity.HasValue,
                    x => x.Quantity <= request.MaxQuantity!.Value)

                .WhereIf(
                    !string.IsNullOrWhiteSpace(search),
                    x =>
                        x.Product.Name.ToLower().Contains(search!) ||
                        x.Product.Sku.Value.ToLower().Contains(search!));
        }
    }
}
