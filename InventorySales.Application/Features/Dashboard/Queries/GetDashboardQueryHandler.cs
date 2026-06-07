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
        .CountAsync(ct);

            var totalCategories =
    await _uow.Repository<Category>()
        .Query()
        .CountAsync(ct);

            var lowStockProducts =
    await _uow.Repository<Product>()
        .Query()
        .CountAsync(x => x.Stock.Value <= 10, ct);

            var today = DateTime.UtcNow.Date;

            var todaySales = await _uow.Repository<Sale>()
        .Query()
        .Where(x => x.CreatedDate >= today)
        .ToListAsync(ct);

            var todaySalesAmount = todaySales.Sum(x => x.TotalPrice.Amount);
            var todaySalesCount = todaySales.Count;

            var recentSales =
    await _uow.Repository<Sale>()
        .Query()
        .OrderByDescending(x => x.CreatedDate)
        .Take(5)
        .ProjectTo<RecentSaleDto>(_mapper.ConfigurationProvider)
        .ToListAsync(ct);

            var recentMovements =
    await _uow.Repository<StockMovement>()
        .Query()
        .OrderByDescending(x => x.CreatedDate)
        .Take(5)
        .ProjectTo<RecentStockMovementDto>(
            _mapper.ConfigurationProvider)
        .ToListAsync(ct);

            return new DashboardDto
            {
                TotalProducts = totalProducts,
                TotalCategories = totalCategories,
                LowStockProducts = lowStockProducts,
                TodaySalesAmount = todaySalesAmount,
                RecentSales = recentSales,
                RecentMovements = recentMovements
            };

        }
    }
}
