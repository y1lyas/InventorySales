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
                .Where(x => x.Product != null);

            if (request.ProductId.HasValue)
            {
                baseQuery = baseQuery.Where(x => x.ProductId == request.ProductId.Value);
            }

            if (request.MovementType.HasValue)
            {
                baseQuery = baseQuery.Where(x =>
                    x.MovementType == request.MovementType.Value);
            }
            if (request.MovementReason.HasValue)
            {
                baseQuery = baseQuery.Where(x =>
                    x.Reason == request.MovementReason.Value);
            }
            if (request.StartDate.HasValue)
            {
                baseQuery = baseQuery.Where(x =>
                    x.CreatedDate >= request.StartDate.Value);
            }
            if (request.EndDate.HasValue)
            {
                var endDate = request.EndDate.Value.Date.AddDays(1);

                baseQuery = baseQuery.Where(x =>
                    x.CreatedDate < endDate);
            }
            if (request.MinQuantity.HasValue)
            {
                baseQuery = baseQuery.Where(x =>
                    x.Quantity >= request.MinQuantity.Value);
            }
            if (request.MaxQuantity.HasValue)
            {
                baseQuery = baseQuery.Where(x =>
                    x.Quantity <= request.MaxQuantity.Value);
            }
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                var search = request.SearchTerm.Trim().ToLower();

                baseQuery = baseQuery.Where(x =>
                    x.Product.Name.ToLower().Contains(search) ||
                    x.Product.Sku.Value.ToLower().Contains(search));
            }

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
