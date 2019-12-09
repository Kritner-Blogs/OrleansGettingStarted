using Orleans;
using System.Threading.Tasks;

namespace Kritner.Orleans.GettingStarted.GrainInterfaces
{
    public interface IEverythingIsOkGrain : IGrainWithStringKey, IRemindable
    {
        Task Start();
        Task Stop();
    }
}
