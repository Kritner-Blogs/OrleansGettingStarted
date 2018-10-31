using Orleans;
using System.Threading.Tasks;

namespace Kritner.OrleansGettingStarted.GrainInterfaces
{
    public interface IVisitTracker : IGrainWithStringKey, IGrainInterfaceMarker
    {
        Task<int> GetNumberOfVisits();
        Task Visit();
    }
}
