using System.Threading.Tasks;
using Orleans;

namespace Kritner.Orleans.GettingStarted.GrainInterfaces;

public interface IHelloWorld : IGrainWithGuidKey, IGrainInterfaceMarker
{
    Task<string> SayHello(string name);
}