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
		
		private readonly IHostEnvironmentStatistics _hostEnvironmentStatistics;

		public CpuHealthCheckGrain(IHostEnvironmentStatistics hostEnvironmentStatistics)
		{
			_hostEnvironmentStatistics = hostEnvironmentStatistics;
		}
		
		public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
		{
			if (_hostEnvironmentStatistics.CpuUsage > UnhealthyThreshold)
			{
				return Task.FromResult(HealthCheckResult.Unhealthy(
					$"CPU utilization is unhealthy at {_hostEnvironmentStatistics.CpuUsage * 100}%."));
				
			}
			
			if (_hostEnvironmentStatistics.CpuUsage > DegradedThreshold)
			{
				return Task.FromResult(HealthCheckResult.Degraded(
					$"CPU utilization is unhealthy at {_hostEnvironmentStatistics.CpuUsage * 100}%."));
			}
			
			return Task.FromResult(HealthCheckResult.Healthy(
				$"CPU utilization is unhealthy at {_hostEnvironmentStatistics.CpuUsage * 100}%."));
		}
	}
}