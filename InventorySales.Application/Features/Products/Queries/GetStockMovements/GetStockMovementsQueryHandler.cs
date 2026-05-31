using InventorySales.Application.Abstractions;
using InventorySales.Application.Abstractions.Services;
using InventorySales.Application.Common.Pagination;
using InventorySales.Application.Features.Products.DTOs;
using InventorySales.Application.Features.Products.Queries.GetStockMovement;
using InventorySales.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventorySales.Application.Features.Products.Queries.GetStockMovements
{
    public class GetStockMovementsQueryHandler
        : IRequestHandler<GetStockMovementsQuery, PagedResult<StockMovementDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IPaginationService _paginationService;

        public GetStockMovementsQueryHandler(IUnitOfWork uow, IMapper mapper, IPaginationService paginationService)
        {
            _uow = uow;
            _mapper = mapper;
            _paginationService = paginationService;
        }

        public async Task<PagedResult<StockMovementDto>> Handle(
     GetStockMovementsQuery request,
     CancellationToken ct)
        {
            var baseQuery = _uow.Repository<StockMovement>()
                .Query()
                .AsNoTracking()
                .Where(x => x.ProductId == request.ProductId);

            return await _paginationService
           .CreateAsync<StockMovement, StockMovementDto>(
               baseQuery,
               q => q.OrderByDescending(x => x.CreatedDate),
               request.PageNumber,
               request.PageSize,
               _mapper.ConfigurationProvider,
               ct);
        }
    }
}
