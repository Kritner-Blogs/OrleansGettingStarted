using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Kritner.Orleans.GettingStarted.GrainInterfaces.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Orleans;
using Orleans.Concurrency;
using Orleans.Statistics;

namespace Kritner.Orleans.GettingStarted.Grains.HealthChecks
{
	[StatelessWorker(1)]
	public class CpuHealthCheckGrain : Grain, ICpuHealthCheckGrain
	{
		private const float UnhealthyThreshold = 90;
		private const float DegradedThreshold = 70;
		private readonly ReadOnlyDictionary<string, object> HealthCheckData = new ReadOnlyDictionary<string, object>(
			new Dictionary<string, object>()
			{
				{ "Unhealthy Threshold",  UnhealthyThreshold},
				{ "Degraded Threshold",  DegradedThreshold}
			});

		private readonly IHostEnvironmentStatistics _hostEnvironmentStatistics;

		public CpuHealthCheckGrain(IHostEnvironmentStatistics hostEnvironmentStatistics)
		{
			_hostEnvironmentStatistics = hostEnvironmentStatistics;
		}

		public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
		{
			if (_hostEnvironmentStatistics.CpuUsage == null)
			{
				return Task.FromResult(HealthCheckResult.Unhealthy("Could not determine CPU usage.", data: HealthCheckData));
			}

			if (_hostEnvironmentStatistics.CpuUsage > UnhealthyThreshold)
			{
				return Task.FromResult(HealthCheckResult.Unhealthy(
					$"CPU utilization is unhealthy at {_hostEnvironmentStatistics.CpuUsage:0.00}%.", data: HealthCheckData));
			}

			if (_hostEnvironmentStatistics.CpuUsage > DegradedThreshold)
			{
				return Task.FromResult(HealthCheckResult.Degraded(
					$"CPU utilization is degraded at {_hostEnvironmentStatistics.CpuUsage:0.00}%.", data: HealthCheckData));
			}

			return Task.FromResult(HealthCheckResult.Healthy(
				$"CPU utilization is healthy at {_hostEnvironmentStatistics.CpuUsage:0.00}%.", data: HealthCheckData));
		}
	}
}