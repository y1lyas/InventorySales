using InventorySales.Application.Abstractions;
using InventorySales.Application.Abstractions.Services;
using InventorySales.Application.Common.Pagination;
using InventorySales.Application.Features.Products.DTOs;
using InventorySales.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventorySales.Application.Features.Products.Queries.GetAllStockMovements
{
    public class GetAllStockMovementsQueryHandler : IRequestHandler<GetAllStockMovementsQuery, PagedResult<StockMovementDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IPaginationService _paginationService;

        public GetAllStockMovementsQueryHandler(IMapper mapper, IUnitOfWork uow, IPaginationService paginationService)
        {
            _mapper = mapper;
            _uow = uow;
            _paginationService = paginationService;
        }

        public async Task<PagedResult<StockMovementDto>> Handle(GetAllStockMovementsQuery request, CancellationToken ct)
        {
            var baseQuery = _uow.Repository<StockMovement>()
                 .Query()
                 .AsNoTracking()
                 .Where(x => x.Product != null)
                 .ApplyFilters(request);

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
