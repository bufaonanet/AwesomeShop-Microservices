using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace AwesomeShop.Services.Orders.Infrastructure.CacheStorage;

public class CacheService : ICacheService
{
    private readonly IDistributedCache _cache;

    public CacheService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<T> GetAsync<T>(string key)
    {
        var objectString = await _cache.GetStringAsync(key);
        if (string.IsNullOrWhiteSpace(objectString))
        {
            Console.WriteLine($"Cache miss for key: {key}");
            return default(T);
        }

        Console.WriteLine($"Cache key found for key {key}");

        return JsonSerializer.Deserialize<T>(objectString);
    }

    public async  Task SetAsync<T>(string key, T data)
    {
        var memoryCacheEntryOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3600),
            SlidingExpiration = TimeSpan.FromSeconds(1200)
        };

        var objectString = JsonSerializer.Serialize(data);

        Console.WriteLine($"cache set for key {key}");

        await _cache.SetStringAsync(key, objectString, memoryCacheEntryOptions);
    }
}