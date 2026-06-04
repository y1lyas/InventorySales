using InventorySales.Application.Abstractions;
using InventorySales.Application.Abstractions.Services;
using InventorySales.Application.Common.Pagination;
using InventorySales.Application.Features.Products.DTOs;
using InventorySales.Application.Features.Sales.DTOs;
using InventorySales.Application.Features.Sales.Queries.GetAllSales;
using InventorySales.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventorySales.Application.Features.Sales.Queries.GetSales
{
    public class GetAllSalesQueryHandler : IRequestHandler<GetAllSalesQuery, PagedResult<SaleDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IUserService _userInfo;
        private readonly IPaginationService _paginationService;
        public GetAllSalesQueryHandler(IUnitOfWork uow, IMapper mapper, IUserService userInfo, IPaginationService paginationService)
        {
            _uow = uow;
            _mapper = mapper;
            _userInfo = userInfo;
            _paginationService = paginationService;
        }

        public async Task<PagedResult<SaleDto>> Handle(GetAllSalesQuery request, CancellationToken cancellationToken)
        {
            var userId = _userInfo.UserId;

            var baseQuery = _uow.Repository<Sale>().Query()
                .Where(u => u.CreatedById == userId)
                .AsNoTracking()
                .ApplyFilters(request);


            return await _paginationService
             .CreateAsync<Sale, SaleDto>(
             baseQuery,
             q => q.OrderByDescending(x => x.CreatedDate),
             request.PageNumber,
             request.PageSize,
             _mapper.ConfigurationProvider,
             cancellationToken);
        }
    }
}
