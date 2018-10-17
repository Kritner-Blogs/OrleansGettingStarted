using Orleans;
using System.Threading.Tasks;

namespace Kritner.OrleansGettingStarted.GrainInterfaces
{
    public interface IVisitTracker : IGrainWithStringKey
    {
        Task<int> GetNumberOfVisits();
        Task Visit();
    }
}
