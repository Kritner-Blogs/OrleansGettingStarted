using System.Threading.Tasks;
using Orleans;

namespace Kritner.Orleans.GettingStarted.GrainInterfaces
{
    public interface IVisitTracker : IGrainWithStringKey, IGrainInterfaceMarker
    {
        Task<int> GetNumberOfVisits();
        Task Visit();
    }
}
