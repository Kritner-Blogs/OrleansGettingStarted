using Orleans;
using System.Threading.Tasks;

namespace Kritner.OrleansGettingStarted.GrainInterfaces
{
    public interface IHelloWorld : IGrainWithGuidKey, IGrainInterfaceMarker
    {
        Task<string> SayHello(string name);
    }
}
