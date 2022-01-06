using System.Threading.Tasks;
using Kritner.Orleans.GettingStarted.Grains;
using Kritner.OrleansGettingStarted.Common.Config;
using Kritner.OrleansGettingStarted.Common.Helpers;
using Kritner.OrleansGettingStarted.SiloHost.ExtensionMethods;
using Kritner.OrleansGettingStarted.SiloHost.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans.Statistics;

namespace Kritner.OrleansGettingStarted.SiloHost
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var (env, configurationRoot, orleansConfig) =
                ConsoleAppConfigurator.BootstrapConfigurationRoot();

            await CreateHostBuilder(args, env, configurationRoot, orleansConfig).Build().RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args, string env, IConfigurationRoot configurationRoot, OrleansConfig orleansConfig)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, builder) =>
                {
                    context.HostingEnvironment.EnvironmentName = env;
                    builder.AddConfiguration(configurationRoot);
                })
                .ConfigureServices(DependencyInjectionHelper.IocContainerRegistration)
                .ConfigureLogging(logging =>
                {
                    logging.AddConsole();
                    logging.SetMinimumLevel(LogLevel.Information);
                })
                .ConfigureWebHostDefaults(builder =>
                {
                    builder.UseStartup<Startup>();
                })
                .UseOrleans(siloBuilder =>
                {
                    siloBuilder
                        .ConfigureClustering(
                            orleansConfig,
                            env
                        )
                        .Configure<ClusterOptions>(options =>
                        {
                            options.ClusterId = "dev";
                            options.ServiceId = "HelloWorldApp";
                        })
                        .AddMemoryGrainStorage(Constants.OrleansMemoryProvider)
                        .ConfigureApplicationParts(parts =>
                        {
                            parts.AddApplicationPart(typeof(IGrainMarker).Assembly).WithReferences();
                        })
                        .UsePerfCounterEnvironmentStatistics()
                        .UseDashboard(options => { })
                        .UseInMemoryReminderService()
                        .ConfigureLogging(logging =>
                        {
                            logging.SetMinimumLevel(LogLevel.Warning);
                            logging.AddConsole();
                        });
                });
        }
    }
}
