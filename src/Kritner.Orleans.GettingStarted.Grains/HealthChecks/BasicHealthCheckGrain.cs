using System.Threading;
using System.Threading.Tasks;
using Kritner.Orleans.GettingStarted.GrainInterfaces.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Orleans;
using Orleans.Concurrency;

namespace Kritner.Orleans.GettingStarted.Grains.HealthChecks
{
	[StatelessWorker(1)]
	public class BasicHealthCheckGrain : Grain, IBasicHealthCheckGrain
	{
		private const string HealthCheckDescription = "A basic health check.  Should only ever return healthy assuming the Orleans cluster is up.";

		public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
		{
			return Task.FromResult(new HealthCheckResult(HealthStatus.Healthy, HealthCheckDescription));
		}
	}
}