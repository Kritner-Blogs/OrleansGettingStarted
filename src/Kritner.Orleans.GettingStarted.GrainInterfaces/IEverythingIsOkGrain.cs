using System.Threading.Tasks;
using Orleans;

namespace Kritner.Orleans.GettingStarted.GrainInterfaces
{
    public interface IEverythingIsOkGrain : IGrainWithStringKey, IRemindable
    {
        Task Start();
        Task Stop();
    }
}
