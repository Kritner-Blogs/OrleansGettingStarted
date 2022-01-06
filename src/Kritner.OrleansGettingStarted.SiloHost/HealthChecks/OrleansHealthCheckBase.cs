using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Orleans;

namespace Kritner.OrleansGettingStarted.SiloHost.HealthChecks;

public abstract class OrleansHealthCheckBase : IHealthCheck
{
    protected readonly IClusterClient _client;

    protected OrleansHealthCheckBase(IClusterClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Entry into health check, ensures the client is initialized, if it is not returns a healthy status.
    /// </summary>
    /// <param name="context">The health check context.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns><see cref="Task"/> of <see cref="HealthCheckResult"/></returns>
    public virtual async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        if (!_client.IsInitialized)
        {
            return HealthCheckResult.Healthy($"{nameof(_client)} not yet initialized.");
        }

        return await CheckHealthGrainAsync(context, cancellationToken);
    }

    /// <summary>
    /// Perform the actual health check work within this implemented method.
    /// </summary>
    /// <param name="context">The health check context.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns><see cref="Task"/> of <see cref="HealthCheckResult"/></returns>
    protected abstract Task<HealthCheckResult> CheckHealthGrainAsync(HealthCheckContext context, CancellationToken cancellationToken);
}