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
	public class MemoryHealthCheckGrain : Grain, IMemoryHealthCheckGrain
	{
		private const float UnhealthyThreshold = 95;
		private const float DegradedThreshold = 90;

		private readonly ReadOnlyDictionary<string, object> HealthCheckData = new ReadOnlyDictionary<string, object>(
			new Dictionary<string, object>()
			{
						{ "Unhealthy Threshold",  UnhealthyThreshold},
						{ "Degraded Threshold",  DegradedThreshold}
			});

		private readonly IHostEnvironmentStatistics _hostEnvironmentStatistics;

		public MemoryHealthCheckGrain(IHostEnvironmentStatistics hostEnvironmentStatistics)
		{
			_hostEnvironmentStatistics = hostEnvironmentStatistics;
		}
		
		public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
		{
			if (_hostEnvironmentStatistics?.AvailableMemory == null || _hostEnvironmentStatistics?.TotalPhysicalMemory == null)
			{
				return Task.FromResult(HealthCheckResult.Unhealthy("Could not determine memory calculation.", data: HealthCheckData));
			}
			
			if (_hostEnvironmentStatistics?.AvailableMemory == 0 && _hostEnvironmentStatistics?.AvailableMemory == 0)
			{
				return Task.FromResult(HealthCheckResult.Unhealthy("Could not determine memory calculation.", data: HealthCheckData));
			}
			
			var memoryUsed = 100 - ((float)_hostEnvironmentStatistics.AvailableMemory / (float)_hostEnvironmentStatistics.TotalPhysicalMemory * 100);
			
			if (memoryUsed > UnhealthyThreshold)
			{
				return Task.FromResult(HealthCheckResult.Unhealthy(
					$"Memory utilization is unhealthy at {memoryUsed:0.00}%.", data: HealthCheckData));
				
			}
			
			if (memoryUsed > DegradedThreshold)
			{
				return Task.FromResult(HealthCheckResult.Degraded(
					$"Memory utilization is degraded at {memoryUsed:0.00}%.", data: HealthCheckData));
			}
			
			return Task.FromResult(HealthCheckResult.Healthy(
				$"Memory utilization is healthy at {memoryUsed:0.00}%.", data: HealthCheckData));
		}
	}
}