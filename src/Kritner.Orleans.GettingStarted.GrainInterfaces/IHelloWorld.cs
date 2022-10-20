using System.Threading.Tasks;
using Orleans;

namespace Kritner.Orleans.GettingStarted.GrainInterfaces;

/// <summary>
/// A hello world grain abstraction.
/// </summary>
public interface IHelloWorld : IGrainWithGuidKey, IGrainInterfaceMarker
{
    /// <summary>
    /// Say hello to a person!
    /// </summary>
    /// <param name="name">The name of the person that Orleans is saying hello to.</param>
    /// <returns>a formatted greeting, personalized to the person!</returns>
    Task<string> SayHello(string name);
}
