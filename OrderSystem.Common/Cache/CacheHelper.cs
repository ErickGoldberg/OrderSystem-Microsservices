using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace OrderSystem.Common.Cache
{
    public class CacheHelper : ICacheService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<CacheHelper> _logger;

        public CacheHelper(IMemoryCache memoryCache, ILogger<CacheHelper> logger)
        {
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            try
            {
                if (_memoryCache.TryGetValue(key, out T? value))
                {
                    _logger.LogInformation("Cache hit for key: {CacheKey}", key);
                    return await Task.FromResult(value);
                }

                _logger.LogWarning("Cache miss for key: {CacheKey}", key);
                return default;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting cache value for key: {CacheKey}", key);
                throw;
            }
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan expiration)
        {
            try
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(expiration);

                _memoryCache.Set(key, value, cacheEntryOptions);
                _logger.LogInformation("Cache set for key: {CacheKey} with expiration: {Expiration}", key, expiration);
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error setting cache value for key: {CacheKey}", key);
                throw;
            }
        }

        public async Task<bool> ExistsAsync(string key)
        {
            try
            {
                var exists = _memoryCache.TryGetValue(key, out _);
                _logger.LogInformation("Cache existence check for key: {CacheKey} - Exists: {Exists}", key, exists);
                return await Task.FromResult(exists);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking cache existence for key: {CacheKey}", key);
                throw;
            }
        }

        public async Task RemoveAsync(string key)
        {
            try
            {
                _memoryCache.Remove(key);
                _logger.LogInformation("Cache removed for key: {CacheKey}", key);
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing cache for key: {CacheKey}", key);
                throw;
            }
        }
    }
}
