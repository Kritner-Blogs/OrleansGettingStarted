using Orleans;
using System;

namespace Kritner.OrleansGettingStarted.GrainInterfaces
{
    public interface IObserverSample : IGrainObserver
    {
        void ReceiveMessage(string message);
    }
}
