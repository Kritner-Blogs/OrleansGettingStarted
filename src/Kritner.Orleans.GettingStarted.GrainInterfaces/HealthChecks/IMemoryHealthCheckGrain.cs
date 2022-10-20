using Microsoft.Extensions.Diagnostics.HealthChecks;
using Orleans;

namespace Kritner.Orleans.GettingStarted.GrainInterfaces.HealthChecks;

/// <summary>
/// Describes a health check grain that reports on the status of memory utilization.
/// </summary>
public interface IMemoryHealthCheckGrain : IHealthCheck, IGrainWithGuidKey
{

}
