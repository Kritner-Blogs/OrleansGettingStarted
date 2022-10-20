using System.Threading.Tasks;
using Orleans;

namespace Kritner.Orleans.GettingStarted.GrainInterfaces;

/// <summary>
/// An abstraction for tracking and reporting on visitors to some "thing".
/// </summary>
public interface IVisitTracker : IGrainWithStringKey, IGrainInterfaceMarker
{
    /// <summary>
    /// Returns the number of visits that have occured.
    /// </summary>
    /// <returns>The number of visits.</returns>
    Task<int> GetNumberOfVisits();
    /// <summary>
    /// Record a visit has occurred.
    /// </summary>
    Task Visit();
}
