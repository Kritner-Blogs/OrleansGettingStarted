using System;
using System.Threading;
using System.Threading.Tasks;
using Kritner.Orleans.GettingStarted.GrainInterfaces.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Orleans;

namespace Kritner.OrleansGettingStarted.SiloHost.HealthChecks
{
	public class CpuOrleansHealthCheck : OrleansHealthCheckBase
	{
		public CpuOrleansHealthCheck(IClusterClient client) : base(client)
		{
		}
		
		protected override async Task<HealthCheckResult> CheckHealthGrainAsync(HealthCheckContext context, CancellationToken cancellationToken)
		{
			try
			{
				return await _client.GetGrain<ICpuHealthCheckGrain>(Guid.Empty)
					.CheckHealthAsync(context, cancellationToken);
			}
			catch (Exception e)
			{
				return HealthCheckResult.Unhealthy($"Health check failed.", e);
			}
		}
	}
}