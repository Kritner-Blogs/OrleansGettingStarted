using System;
using Orleans;

namespace Kritner.Orleans.GettingStarted.GrainInterfaces;

/// <summary>
/// An abstraction grain that allows observers to receive messages.
/// </summary>
public interface IObserverSample : IGrainObserver
{
    /// <summary>
    /// Receive a message on an observer.
    /// </summary>
    /// <param name="message">The message to receive.</param>
    void ReceiveMessage(string message);
}
