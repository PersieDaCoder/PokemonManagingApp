using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using PokemonManagingApp.Core.Interfaces.Caching;

namespace PokemonManagingApp.Infrastructure.Data.Caching;

public class CacheService(IMemoryCache memoryCache) : ICacheService
{
    private readonly IMemoryCache _memoryCache = memoryCache;

    public T? GetData<T>(string key)
    {
        if(key.IsNullOrEmpty()) return default(T);
        return _memoryCache.Get<T>(key);
    }

    public void RemoveData(string key)
    {
        if(key.IsNullOrEmpty()) return;
        _memoryCache.Remove(key);
    }

    public void SetData<T>(string key, T value, TimeSpan expirationTime)
    {
        if(key.IsNullOrEmpty()) return;
        _memoryCache.Set<T>(key, value, expirationTime);
    }
}