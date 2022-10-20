using System.Threading.Tasks;
using Orleans;

namespace Kritner.Orleans.GettingStarted.GrainInterfaces;

/// <summary>
/// An grain abstraction that can used as an "Everything is ok" reminder.
/// </summary>
public interface IEverythingIsOkGrain : IGrainWithStringKey, IRemindable
{
    /// <summary>
    /// Start the everything is ok service.
    /// </summary>
    Task Start();
    /// <summary>
    /// Stop the everything is ok service.
    /// </summary>
    Task Stop();
}
