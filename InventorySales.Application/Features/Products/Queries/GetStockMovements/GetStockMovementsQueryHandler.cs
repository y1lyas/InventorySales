using AutoMapper.QueryableExtensions;
using InventorySales.Application.Abstractions;
using InventorySales.Application.Attributes;
using InventorySales.Application.Features.Interfaces;
using InventorySales.Application.Features.Products.DTOs;
using InventorySales.Application.Features.Products.Paging;
using InventorySales.Application.Features.Products.Queries.GetStockMovement;
using InventorySales.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Products.Queries.GetStockMovements
{
    public class GetStockMovementsQueryHandler
        : IRequestHandler<GetStockMovementsQuery, PagedResult<StockMovementDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetStockMovementsQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<PagedResult<StockMovementDto>> Handle(GetStockMovementsQuery request, CancellationToken ct)
        {
            var pageNumber = Math.Max(1, request.PageNumber);
            var pageSize = Math.Clamp(request.PageSize, 1, 200);

            var queryable = _uow.Repository<StockMovement>()
                .Query()
                .AsNoTracking()
                .Where(x => x.ProductId == request.ProductId)
                .OrderByDescending(x => x.CreatedDate);

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
