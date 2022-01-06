using System.Threading.Tasks;
using Kritner.Orleans.GettingStarted.GrainInterfaces;
using Orleans;

namespace Kritner.Orleans.GettingStarted.Grains
{
    public class HelloWorld : Grain, IHelloWorld, IGrainMarker
    {
        public Task<string> SayHello(string name)
        {
            return Task.FromResult($"Hello from grain {this.GetGrainIdentity()}, {name}!");
        }
    }
}
