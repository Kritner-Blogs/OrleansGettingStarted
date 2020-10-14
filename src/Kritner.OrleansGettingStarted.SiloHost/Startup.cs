using Kritner.OrleansGettingStarted.SiloHost.HealthChecks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Kritner.OrleansGettingStarted.SiloHost
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			services.AddHealthChecks()
				.AddCheck<BasicOrleansHealthCheck>("basicOrleans")
				.AddCheck<CpuOrleansHealthCheck>("cpuOrleans")
				.AddCheck<MemoryOrleansHealthCheck>("memoryOrleans");
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapHealthChecks("/health").WithMetadata(new AllowAnonymousAttribute());
				endpoints.MapControllers();
			});
		}
	}
}