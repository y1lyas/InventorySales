using AutoMapper.QueryableExtensions;
using InventorySales.Application.Abstractions;
using InventorySales.Application.Features.Products.DTOs;
using InventorySales.Application.Features.Products.Paging;
using InventorySales.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Products.Queries.GetAllStockMovements
{
    public class GetAllStockMovementsQueryHandler : IRequestHandler<GetAllStockMovementsQuery, PagedResult<StockMovementDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public GetAllStockMovementsQueryHandler(IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<PagedResult<StockMovementDto>> Handle(GetAllStockMovementsQuery request, CancellationToken ct)
        {
            var pageNumber = Math.Max(1, request.PageNumber);
            var pageSize = Math.Clamp(request.PageSize, 1, 200);
            var queryable = _uow.Repository<StockMovement>()
                .Query()
                .AsNoTracking();

            if (request.ProductId.HasValue)
            {
                queryable = queryable.Where(x => x.ProductId == request.ProductId.Value);
            }

            if (request.MovementType.HasValue)
            {
                queryable = queryable.Where(x =>
                    x.MovementType == request.MovementType.Value);
            }

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                var search = request.SearchTerm.Trim().ToLower();

                queryable = queryable.Where(x =>
                    x.Product.Name.ToLower().Contains(search) ||
                    x.Product.Sku.Value.ToLower().Contains(search));
            }

            queryable = queryable.OrderByDescending(x => x.CreatedDate);

            var totalCount = await queryable.LongCountAsync(ct);

            var items = await queryable
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<StockMovementDto>(_mapper.ConfigurationProvider)
                .ToListAsync(ct);

            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            return new PagedResult<StockMovementDto>(
                items,
                pageNumber,
                pageSize,
                totalCount,
                totalPages
            );
        }
    }
}
