using InventorySales.Application.Abstractions;
using InventorySales.Application.Abstractions.Services;
using InventorySales.Application.Features.Sales.DTOs;
using InventorySales.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InventorySales.Application.Features.Sales.Queries.GetSales
{
    public class GetSalesQueryHandler : IRequestHandler<GetSalesQuery, List<SaleDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IUserService _userInfo;
        private readonly ILogger<GetSalesQueryHandler> _logger;


        public GetSalesQueryHandler(IUnitOfWork uow, IMapper mapper, IUserService userInfo, ILogger<GetSalesQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _userInfo = userInfo;
            _logger = logger;
        }

        public async Task<List<SaleDto>> Handle(GetSalesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetSales requested");

            var userId = _userInfo.UserId;

            var sales = await _uow.Repository<Sale>().Query()
                .Where(u => u.CreatedById == userId)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<SaleDto>>(sales);
        }
    }
}
