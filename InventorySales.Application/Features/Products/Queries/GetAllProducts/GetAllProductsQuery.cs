using InventorySales.Application.Abstractions.RedisCache;
using InventorySales.Application.Features.Products.DTOs;

namespace InventorySales.Application.Features.Products.Queries.GetProducts
{
    public class GetAllProductsQuery() : IRequest<List<ProductDto>>, ICacheableQuery
    {
        public string CacheKey => "GetAllProducts";

        public TimeSpan CacheDuration => TimeSpan.FromMinutes(1);
    }


}
