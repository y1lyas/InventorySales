using InventorySales.Application.Features.Products.DTOs;
using InventorySales.Application.Interfaces;
using InventorySales.Domain.Entities;

namespace InventorySales.Application.Features.Products.Queries.GetProducts
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<ProductDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public GetProductsQueryHandler(IUnitOfWork uow, IMapper mapper, IUserService userService)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<List<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _uow.Repository<Product>().GetAllAsync();
            return _mapper.Map<List<ProductDto>>(products);
        }
    }
}
