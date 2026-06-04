using InventorySales.Application.Common.Extensions;
using InventorySales.Application.Features.Products.Queries.GetProducts;
using InventorySales.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Products.Queries.GetAllProducts
{
    public static class ProductFilteringExtensions
    {
        public static IQueryable<Product> ApplyFilters(
            this IQueryable<Product> query,
            GetAllProductsQuery request)
        {
            if (request.IsDeleted.GetValueOrDefault())
            {
                query = query
                    .IgnoreQueryFilters()
                    .Where(x => x.IsDeleted)
                    .OrderByDescending(x => x.DeletedAt);
            }

            var search = request.SearchTerm?.Trim().ToLower();

            return query
                .WhereIf(
                    request.CategoryId.HasValue,
                    x => x.CategoryId == request.CategoryId!.Value)

                .WhereIf(
                    !string.IsNullOrWhiteSpace(search),
                    x =>
                        x.Name.ToLower().Contains(search!) ||
                        x.Sku.Value.ToLower().Contains(search!))

                .WhereIf(
                    request.MinStock.HasValue,
                    x => x.Stock.Value >= request.MinStock!.Value)

                .WhereIf(
                    request.MaxStock.HasValue,
                    x => x.Stock.Value <= request.MaxStock!.Value)

                .WhereIf(
                    request.MinPrice.HasValue,
                    x => x.Price.Amount >= request.MinPrice!.Value)

                .WhereIf(
                    request.MaxPrice.HasValue,
                    x => x.Price.Amount <= request.MaxPrice!.Value)

                .WhereIf(
                    request.StartDate.HasValue,
                    x => x.CreatedDate >= request.StartDate!.Value)

                .WhereIf(
                    request.EndDate.HasValue,
                    x => x.CreatedDate < request.EndDate!.Value.Date.AddDays(1));
        }
    }
}
