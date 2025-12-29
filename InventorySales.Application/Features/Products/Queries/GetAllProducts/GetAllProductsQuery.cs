using InventorySales.Application.Abstractions.RedisCache;
using InventorySales.Application.Features.Products.DTOs;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Products.Queries.GetProducts
{
    public class GetAllProductsQuery() : IRequest<List<ProductDto>>, ICacheableQuery
    {
        public string CacheKey => "GetAllProducts";

        public TimeSpan CacheDuration => TimeSpan.FromMinutes(1);

        public string LockKey => $"lock:cache:task:GetAllProducts";

        public TimeSpan LockDuration => TimeSpan.FromSeconds(3);
    }


}
