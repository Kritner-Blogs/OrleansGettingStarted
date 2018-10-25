using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kritner.OrleansGettingStarted.Client.OrleansFunctionExamples
{
    public interface IOrleansFunction
    {
        string Description { get; }
        Task PerformFunction(IClusterClient clusterClient);
    }
}
