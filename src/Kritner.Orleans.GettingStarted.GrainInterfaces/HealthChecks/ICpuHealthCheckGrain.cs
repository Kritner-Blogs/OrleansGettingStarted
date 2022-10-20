using Microsoft.Extensions.Diagnostics.HealthChecks;
using Orleans;

namespace Kritner.Orleans.GettingStarted.GrainInterfaces.HealthChecks;

/// <summary>
/// Describes a health check grain that reports on the status of the CPU.
/// </summary>
public interface ICpuHealthCheckGrain : IHealthCheck, IGrainWithGuidKey
{

}
