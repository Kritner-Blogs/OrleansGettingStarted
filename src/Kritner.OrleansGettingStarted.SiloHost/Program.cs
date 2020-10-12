using Kritner.OrleansGettingStarted.Common.Config;
using Kritner.Orleans.GettingStarted.Grains;
using Kritner.OrleansGettingStarted.SiloHost.ExtensionMethods;
using Kritner.OrleansGettingStarted.SiloHost.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans.Statistics;
using System;
using System.Threading.Tasks;
using Kritner.OrleansGettingStarted.Common.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

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
                            .ConfigureServices(DependencyInjectionHelper.IocContainerRegistration)
                            .UsePerfCounterEnvironmentStatistics()
                            .UseDashboard(options => { })
                            .UseInMemoryReminderService()
                            .ConfigureLogging(logging => logging.AddConsole());
                });
        }
    }
}
