using Kritner.OrleansGettingStarted.GrainInterfaces;
using Orleans;
using System.Threading.Tasks;

namespace Kritner.OrleansGettingStarted.Grains
{
    public class HelloWorld : Grain, IHelloWorld, IGrainMarker
    {
        public Task<string> SayHello(string name)
        {
            return Task.FromResult($"Hello from grain {this.GetGrainIdentity()}, {name}!");
        }
    }
}
