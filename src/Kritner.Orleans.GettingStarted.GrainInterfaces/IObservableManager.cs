using System.Threading.Tasks;
using Orleans;

namespace Kritner.Orleans.GettingStarted.GrainInterfaces
{
    public interface IObservableManager : IGrainWithIntegerKey, IGrainInterfaceMarker
    {
        Task Subscribe(IObserverSample observer);
        Task Unsubscribe(IObserverSample observer);
        Task SendMessageToObservers(string message);
    }
}
