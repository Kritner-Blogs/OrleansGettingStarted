using System.Threading.Tasks;
using Orleans;

namespace Kritner.Orleans.GettingStarted.GrainInterfaces;

/// <summary>
/// Abstraction supports the (un)subing to an observable, as well as sending messages to observers.
/// </summary>
public interface IObservableManager : IGrainWithIntegerKey, IGrainInterfaceMarker
{
    /// <summary>
    /// Subscribes the provided observer.
    /// </summary>
    /// <param name="observer">The observer to subscribe to the manager.</param>
    Task Subscribe(IObserverSample observer);
    /// <summary>
    /// Unsubscribe the provided observer.
    /// </summary>
    /// <param name="observer">The observer to unsubscribe from the manager.</param>
    Task Unsubscribe(IObserverSample observer);
    /// <summary>
    /// Sends a message to all subscribers.
    /// </summary>
    /// <param name="message">The message to send.</param>
    Task SendMessageToObservers(string message);
}
