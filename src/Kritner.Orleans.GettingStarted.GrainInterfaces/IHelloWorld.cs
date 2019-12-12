using Orleans;
using System.Threading.Tasks;

namespace Kritner.Orleans.GettingStarted.GrainInterfaces
{
    public interface IHelloWorld : IGrainWithGuidKey, IGrainInterfaceMarker
    {
        Task<string> SayHello(string name);
    }
}
