using AutoMapper.QueryableExtensions;
using InventorySales.Application.Abstractions;
using InventorySales.Application.Features.Sales.DTOs;
using InventorySales.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Sales.Queries.GetSale
{
    public class GetSalesQueryHandler : IRequestHandler<GetSalesQuery, SaleDetailDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetSalesQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<SaleDetailDto> Handle(GetSalesQuery request, CancellationToken ct)
        {
            var sale = await _uow.Repository<Sale>()
          .Query()
          .AsNoTracking()
          .Where(x => x.Id == request.SaleId)
          .ProjectTo<SaleDetailDto>(
              _mapper.ConfigurationProvider)
          .FirstOrDefaultAsync(ct);

            return sale
     ?? throw new KeyNotFoundException(
         $"Sale with id {request.SaleId} not found.");  
        }
    }
}
