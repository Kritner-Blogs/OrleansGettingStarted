using Microsoft.Extensions.Diagnostics.HealthChecks;
using Orleans;

namespace Kritner.Orleans.GettingStarted.GrainInterfaces.HealthChecks
{
	public interface ICpuHealthCheckGrain : IHealthCheck, IGrainWithGuidKey
	{
		
	}
}