using System.Threading.Tasks;
using Orleans;

namespace Kritner.Orleans.GettingStarted.GrainInterfaces;

/// <summary>
/// <para>
/// Exposes a cache for use with Orleans.  This cache brings up a new grain per cache key.  The individual grains themselves
/// are backed by an LRU cache, which itself is a dictionary of key value pairs.
/// </para>
/// <para>
/// A use case of this could be to bring up different individually scoped caches based on some arbitrary string key.
/// Each individual cache contains its own set of unique key value pairs.
/// </para>
/// </summary>
/// <typeparam name="TValue">The type of value to be stored within the cache.</typeparam>
public interface IOrleansCache<TValue> : IGrainWithStringKey, IGrainInterfaceMarker
{
    /// <summary>
    /// Adds or updates a value for the specified key.
    /// </summary>
    Task AddOrUpdate(string key, TValue value);
    /// <summary>
    /// Gets a value by the specified key
    /// </summary>
    Task<TValue> Get(string key);
}
