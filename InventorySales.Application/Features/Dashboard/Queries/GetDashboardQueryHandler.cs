using AutoMapper.QueryableExtensions;
using InventorySales.Application.Abstractions;
using InventorySales.Application.Abstractions.Services;
using InventorySales.Application.Features.Dashboard.DTOs;
using InventorySales.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Dashboard.Queries
{
    public class GetDashboardQueryHandler : IRequestHandler<GetDashboardQuery, DashboardDto>
    {

        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetDashboardQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<DashboardDto> Handle(GetDashboardQuery request, CancellationToken ct)
        {
            var totalProducts =
    await _uow.Repository<Product>()
        .Query()
         .AsNoTracking()
        .CountAsync(ct);

            var totalStockQuantity =
    await _uow.Repository<Product>()
        .Query()
        .AsNoTracking()
        .SumAsync(x => x.Stock.Value, ct);

            var totalCategories =
    await _uow.Repository<Category>()
        .Query()
        .AsNoTracking()
        .CountAsync(ct);

            var lowStockProducts =
    await _uow.Repository<Product>()
        .Query()
        .AsNoTracking()
        .CountAsync(x => x.Stock.Value <= 10, ct);

            const int lowStockThreshold = 10;

            var lowStockProductsList =
                await _uow.Repository<Product>()
                    .Query()
                    .AsNoTracking()
                    .Where(x => x.Stock.Value <= lowStockThreshold)
                    .OrderBy(x => x.Stock.Value)
                    .Take(5)
                    .ProjectTo<LowStockProductDto>(
                        _mapper.ConfigurationProvider)
                    .ToListAsync(ct);

            var outOfStockProducts =
    await _uow.Repository<Product>()
        .Query()
        .AsNoTracking()
        .CountAsync(x => x.Stock.Value == 0, ct);

            var today = DateTime.UtcNow.Date;

            var todaySalesQuery = _uow.Repository<Sale>()
             .Query()
             .AsNoTracking()
             .Where(x => x.CreatedDate >= today);

            var todaySalesCount = await todaySalesQuery.CountAsync(ct);
            var todaySalesAmount = await todaySalesQuery
                .SumAsync(x => x.TotalPrice.Amount, ct);

            var recentSales =
    await _uow.Repository<Sale>()
        .Query()
        .AsNoTracking()
        .OrderByDescending(x => x.CreatedDate)
        .Take(5)
        .ProjectTo<RecentSaleDto>(_mapper.ConfigurationProvider)
        .ToListAsync(ct);

            var recentMovements =
    await _uow.Repository<StockMovement>()
        .Query()
        .AsNoTracking()
        .OrderByDescending(x => x.CreatedDate)
        .Take(5)
        .ProjectTo<RecentStockMovementDto>(
            _mapper.ConfigurationProvider)
        .ToListAsync(ct);

            return new DashboardDto
            {
                TotalProducts = totalProducts,
                TotalCategories = totalCategories,
                TotalStockQuantity = totalStockQuantity,
                LowStockProducts = lowStockProducts,
                OutOfStockProducts = outOfStockProducts,
                TodaySalesAmount = todaySalesAmount,
                TodaySalesCount = todaySalesCount,
                LowStockProductsList = lowStockProductsList,
                RecentSales = recentSales,
                RecentMovements = recentMovements
            };

        }
    }
}
