using InventorySales.Application.Abstractions;
using InventorySales.Application.Abstractions.Services;
using InventorySales.Application.Features.Sales.DTOs;
using InventorySales.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventorySales.Application.Features.Sales.Queries.GetSales
{
    public class GetSalesQueryHandler : IRequestHandler<GetSalesQuery, List<SaleDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IUserService _userInfo;

        public GetSalesQueryHandler(IUnitOfWork uow, IMapper mapper, IUserService userInfo)
        {
            _uow = uow;
            _mapper = mapper;
            _userInfo = userInfo;
        }

        public async Task<List<SaleDto>> Handle(GetSalesQuery request, CancellationToken cancellationToken)
        {
            var userId = _userInfo.UserId;

            var sales = await _uow.Repository<Sale>().Query().Where(u => u.CreatedById == userId).ToListAsync(cancellationToken);

            return _mapper.Map<List<SaleDto>>(sales);
        }
    }
}
