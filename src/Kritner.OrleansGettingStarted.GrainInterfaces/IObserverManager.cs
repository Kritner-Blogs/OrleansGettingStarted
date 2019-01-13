using Orleans;
using System.Threading.Tasks;

namespace Kritner.OrleansGettingStarted.GrainInterfaces
{
    public interface IObserverManager : IGrainWithIntegerKey, IGrainInterfaceMarker
    {
        Task Subscribe(IObserverSample observer);
        Task Unsubscribe(IObserverSample observer);
        Task SendMessageToObservers(string message);
    }
}
