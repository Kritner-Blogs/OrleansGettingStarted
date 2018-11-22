using Orleans;
using System.Threading.Tasks;

namespace Kritner.OrleansGettingStarted.GrainInterfaces
{
    public interface IEverythingIsOkGrain : IGrainWithStringKey, IRemindable
    {
        Task Start();
        Task Stop();
    }
}
