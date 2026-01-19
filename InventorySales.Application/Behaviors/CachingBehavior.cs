using InventorySales.Application.Abstractions.RedisCache;
using InventorySales.Application.Attributes;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace InventorySales.Application.Behaviors
{
    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ICacheService _cacheService;
        private readonly ILogger<CachingBehavior<TRequest, TResponse>> _logger;
        private readonly ICacheKeyGenerator _keyGenerator;


        public CachingBehavior(ICacheService cacheService, ILogger<CachingBehavior<TRequest, TResponse>> logger, ICacheKeyGenerator keyGenerator)
        {
            _cacheService = cacheService;
            _logger = logger;
            _keyGenerator = keyGenerator;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var attr = request.GetType().GetCustomAttribute<CacheableAttribute>();
            if (attr is null)
                return await next();

            var key = _keyGenerator.Generate(request);

            _logger.LogDebug(
            "Cacheable request detected. Request={Request}, Key={Key}",
            request.GetType().Name,
            key);

            var cached = await _cacheService.GetAsync<TResponse>(key);
            if (cached != null)
            {
                _logger.LogInformation("Cache HIT for key: {Key}", key);
                return cached;
            }
            _logger.LogInformation("Cache MISS for key: {Key}", key);

            var response = await next();

            await _cacheService.SetAsync(key, response, attr.TtlSeconds, attr.Tags);


            return response;
        }
    }
}
