using System;
using Orleans;

namespace Kritner.Orleans.GettingStarted.GrainInterfaces;

public interface IObserverSample : IGrainObserver
{
    void ReceiveMessage(string message);
}