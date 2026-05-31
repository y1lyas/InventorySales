using InventorySales.Application.Abstractions.RedisCache;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<CacheService> _logger;
        private readonly CacheSettings _cacheSettings;

        public CacheService(IConnectionMultiplexer redis, IOptions<CacheSettings> options, ILogger<CacheService> logger)
        {
            _db = redis.GetDatabase();
            _cacheSettings = options.Value;
            _logger = logger;
        }
        private static string GetTagKey(string tag)
     => $"tag:{tag}";

        public async Task<T?> GetAsync<T>(string cacheKey)
        {
            try
            {
                var value = await _db.StringGetAsync(cacheKey);

                if (value.IsNullOrEmpty)
                {
                    return default;
                }
                var result = JsonSerializer.Deserialize<T>(value);
                return result;
            }
            catch (RedisException ex)
            {
                _logger.LogError(ex, "Redis error while getting cache key: {CacheKey}", cacheKey);
                return default;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JSON deserialization error for cache key: {CacheKey}", cacheKey);
                await RemoveAsync(cacheKey); 
                return default;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while getting cache key: {CacheKey}", cacheKey);
                return default;
            }
        }
        public async Task SetAsync<T>(string cacheKey, T value, TimeSpan ttl, params string[] tags)
        {
            if (ttl <= TimeSpan.Zero)
            {
                _logger.LogWarning("Invalid TTL ({TTL}) for cache key: {CacheKey}", ttl, cacheKey);
                return;
            }
            try
            {
                var json = JsonSerializer.Serialize(value);
                var setSuccess = await _db.StringSetAsync(key: cacheKey, value: json, expiry: ttl);

                if (!setSuccess)
                {
                    _logger.LogWarning("Failed to set cache key: {CacheKey}", cacheKey);
                    return;
                }

                _logger.LogDebug("Cache SET for key: {CacheKey} with TTL: {TTL}", cacheKey, ttl);

                if (tags is null || tags.Length == 0)
                    return;

                var tagTtl = ttl.Add(TimeSpan.FromMinutes(5));

                foreach (var tag in tags)
                {
                    var tagKey = GetTagKey(tag);
                    await _db.SetAddAsync(tagKey, cacheKey);
                    var currentTtl = await _db.KeyTimeToLiveAsync(tagKey);
                    if (!currentTtl.HasValue || currentTtl.Value < tagTtl)
                    {
                        await _db.KeyExpireAsync(tagKey, tagTtl);
                    }
                }

                _logger.LogDebug("Cache tags added for key {CacheKey}: {Tags}", cacheKey, string.Join(", ", tags));
            }
            catch (RedisException ex)
            {
                _logger.LogError(ex, "Redis error while setting cache key: {CacheKey}", cacheKey);
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JSON serialization error for cache key: {CacheKey}", cacheKey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while setting cache key: {CacheKey}", cacheKey);
            }
        }

        public async Task RemoveAsync(string cacheKey)
        {
            if (string.IsNullOrWhiteSpace(cacheKey))
            {
                _logger.LogWarning("Attempted to remove cache with null or empty key");
                return;
            }

            await _db.KeyDeleteAsync(cacheKey);
        }

        public async Task RemoveByTagAsync(string tag)
        {
            if (string.IsNullOrWhiteSpace(tag))
            {
                _logger.LogWarning("Attempted to remove cache by null or empty tag");
                return;
            }

            var tagKey = GetTagKey(tag);
            var keys = await _db.SetMembersAsync(tagKey);

            if (keys.Length == 0)
            {
                _logger.LogDebug("No cache keys found for tag: {Tag}", tag);
                return;
            }

            var redisKeys = keys.Select(x => (RedisKey)x.ToString()).ToArray();

            await _db.KeyDeleteAsync(redisKeys);
            await _db.KeyDeleteAsync(tagKey);

            _logger.LogDebug("Cache invalidated for tag: {Tag}", tag);

        }
    }
}
