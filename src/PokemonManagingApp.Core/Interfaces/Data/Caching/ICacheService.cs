namespace PokemonManagingApp.Core.Interfaces.Caching;

public interface ICacheService
{
    T? GetData<T>(string key);
    void SetData<T>(string key, T value);
    void RemoveData(string key);
}