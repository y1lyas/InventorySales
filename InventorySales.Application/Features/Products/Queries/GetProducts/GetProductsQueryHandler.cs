using InventorySales.Application.Features.Products.DTOs;
using InventorySales.Application.Interfaces;
using InventorySales.Domain.Entities;
using InventorySales.Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Products.Queries.GetProducts
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<ProductDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public GetProductsQueryHandler(IUnitOfWork uow, IMapper mapper, IUserService userService)
        {
            _uow = uow;
            _mapper = mapper;
            _userService = userService;
        }
        public async Task<List<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _uow.Repository<Product>().GetAllAsync();
            return _mapper.Map<List<ProductDto>>(products);
        }
    }
}
