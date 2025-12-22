using InventorySales.Application.Abstractions.RedisCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Behaviors
{
    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ICacheableQuery
    {
        private readonly ICacheService _cacheService;
        public CachingBehavior(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var cachedResponse = await _cacheService.GetAsync<TResponse>(request.CacheKey);
            if (cachedResponse != null)
            {
                return cachedResponse;
            }
            var response = await next();
            await _cacheService.SetAsync(request.CacheKey, response, request.CacheDuration);
            return response;
        }
    }
}
