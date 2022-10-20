using System.Threading.Tasks;
using BitFaster.Caching.Lru;
using Kritner.Orleans.GettingStarted.GrainInterfaces;
using Orleans;

namespace Kritner.Orleans.GettingStarted.Grains;

public class OrleansLruCache<TValue> : Grain, IOrleansCache<TValue>
{
    private readonly ConcurrentLru<string, TValue> _cache;

    public OrleansLruCache()
    {
        _cache = new ConcurrentLru<string, TValue>(50);
    }

    public Task AddOrUpdate(string key, TValue value)
    {
        _cache.AddOrUpdate(key, value);
        return Task.CompletedTask;
    }

    public Task<TValue> Get(string key)
    {
        if (_cache.TryGet(key, out var result))
            return Task.FromResult(result);

        TValue nullResult = default;
        return Task.FromResult(nullResult);
    }
}
