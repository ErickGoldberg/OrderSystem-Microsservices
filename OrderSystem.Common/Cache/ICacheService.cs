namespace OrderSystem.Common.Cache
{
    public interface ICacheService
    {
        Task<T?> GetAsync<T>(string key);
        Task SetAsync<T>(string key, T value, TimeSpan expiration);
        Task<bool> ExistsAsync(string key);
        Task RemoveAsync(string key);
    }
}
