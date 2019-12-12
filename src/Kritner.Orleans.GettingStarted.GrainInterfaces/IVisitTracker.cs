using Orleans;
using System.Threading.Tasks;

namespace Kritner.Orleans.GettingStarted.GrainInterfaces
{
    public interface IVisitTracker : IGrainWithStringKey, IGrainInterfaceMarker
    {
        Task<int> GetNumberOfVisits();
        Task Visit();
    }
}
