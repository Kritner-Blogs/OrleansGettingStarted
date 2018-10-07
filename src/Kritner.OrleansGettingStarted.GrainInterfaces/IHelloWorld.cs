using Orleans;
using System.Threading.Tasks;

namespace Kritner.OrleansGettingStarted.GrainInterfaces
{
    public interface IHelloWorld : IGrainWithGuidKey
    {
        Task<string> SayHello(string name);
    }
}
