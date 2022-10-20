using Microsoft.Extensions.Diagnostics.HealthChecks;
using Orleans;

namespace Kritner.Orleans.GettingStarted.GrainInterfaces.HealthChecks;

/// <summary>
/// Describes a basic health grain check.
/// </summary>
public interface IBasicHealthCheckGrain : IHealthCheck, IGrainWithGuidKey
{

}
