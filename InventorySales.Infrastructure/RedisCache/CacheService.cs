using InventorySales.Application.Abstractions.RedisCache;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InventorySales.Infrastructure.RedisCache
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _db;
        private readonly ConnectionMultiplexer _redisConnection;
        private readonly CacheSettings _cacheSettings;

        public CacheService(IOptions<CacheSettings> options)
        {
            _cacheSettings = options.Value; 
            var optionsConfig = ConfigurationOptions.Parse(_cacheSettings.ConnectionString);
            _redisConnection = ConnectionMultiplexer.Connect(optionsConfig);
            _db = _redisConnection.GetDatabase();
        }
        public async Task<T?> GetAsync<T>(string cacheKey)
        {
            var value = await _db.StringGetAsync(cacheKey);
            return value.HasValue
                ? JsonSerializer.Deserialize<T>(value!)
                : default;
        }

        public async Task SetAsync<T>(string cacheKey, T value, TimeSpan ttl)
        {
            if (ttl <= TimeSpan.Zero)
                return;

            var json = JsonSerializer.Serialize(value);
            await _db.StringSetAsync(key: cacheKey, value: json, expiry: ttl);
        }
    }
}
