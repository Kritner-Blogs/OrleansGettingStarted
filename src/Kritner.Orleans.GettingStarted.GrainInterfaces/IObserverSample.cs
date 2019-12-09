using Orleans;
using System;

namespace Kritner.Orleans.GettingStarted.GrainInterfaces
{
    public interface IObserverSample : IGrainObserver
    {
        void ReceiveMessage(string message);
    }
}
