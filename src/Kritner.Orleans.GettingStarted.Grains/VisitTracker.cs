using System;
using System.Threading.Tasks;
using Kritner.Orleans.GettingStarted.GrainInterfaces;
using Orleans;
using Orleans.Providers;

namespace Kritner.Orleans.GettingStarted.Grains
{
    [StorageProvider(ProviderName = Constants.OrleansMemoryProvider)]
    public class VisitTracker : Grain<VisitTrackerState>, IVisitTracker, IGrainMarker
    {
        public Task<int> GetNumberOfVisits()
        {
            return Task.FromResult(State.NumberOfVisits);
        }

        public async Task Visit()
        {
            var now = DateTime.Now;

            if (!State.FirstVisit.HasValue)
            {
                State.FirstVisit = now;
            }

            State.NumberOfVisits++;
            State.LastVisit = now;

            await WriteStateAsync();
        }
    }

    public class VisitTrackerState
    {
        public DateTime? FirstVisit { get; set; }
        public DateTime? LastVisit { get; set; }
        public int NumberOfVisits { get; set; }
    }
}
