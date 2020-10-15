using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Kritner.OrleansGettingStarted.SiloHost.HealthChecks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
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
				endpoints.MapHealthChecks(
						"/health",
						new HealthCheckOptions
						{
							AllowCachingResponses = false,
							ResponseWriter = WriteResponse
						})
					.WithMetadata(new AllowAnonymousAttribute());
				endpoints.MapControllers();
			});
		}

		private static Task WriteResponse(HttpContext context, HealthReport result)
		{
			context.Response.ContentType = "application/json; charset=utf-8";

			var options = new JsonWriterOptions
			{
				Indented = true
			};

			using var stream = new MemoryStream();
			using (var writer = new Utf8JsonWriter(stream, options))
			{
				writer.WriteStartObject();
				writer.WriteString("status", result.Status.ToString());
				writer.WriteStartObject("results");
				foreach (var entry in result.Entries)
				{
					writer.WriteStartObject(entry.Key);
					writer.WriteString("status", entry.Value.Status.ToString());
					writer.WriteString("description", entry.Value.Description);
					writer.WriteStartObject("data");
					foreach (var item in entry.Value.Data)
					{
						writer.WritePropertyName(item.Key);
						JsonSerializer.Serialize(
							writer, item.Value, item.Value?.GetType() ??
							                    typeof(object));
					}
					writer.WriteEndObject();
					writer.WriteEndObject();
				}
				writer.WriteEndObject();
				writer.WriteEndObject();
			}

			var json = Encoding.UTF8.GetString(stream.ToArray());

			return context.Response.WriteAsync(json);
		}
	}
}