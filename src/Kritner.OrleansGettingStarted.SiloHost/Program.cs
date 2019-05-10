using Kritner.OrleansGettingStarted.Common;
using Kritner.OrleansGettingStarted.Common.Config;
using Kritner.OrleansGettingStarted.Common.Helpers;
using Kritner.OrleansGettingStarted.Grains;
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

namespace Kritner.OrleansGettingStarted.SiloHost
{
    public class Program
    {
        private static Startup Startup;
        private static IServiceProvider ServiceProvider;

        public static int Main(string[] args)
        {
            Startup = ConsoleAppConfigurator.BootstrapApp();
            var serviceCollection = new ServiceCollection();
            Startup.ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();

            return RunMainAsync().Result;
        }

        private static async Task<int> RunMainAsync()
        {
            try
            {
                var host = await StartSilo();
                Console.WriteLine("Press Enter to terminate...");
                Console.ReadLine();

                await host.StopAsync();

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return 1;
            }
        }

        private static async Task<ISiloHost> StartSilo()
        {
            // define the cluster configuration
            var builder = new SiloHostBuilder()
                .ConfigureClustering(
                    ServiceProvider.GetService<IOptions<OrleansConfig>>(),
                    Startup.HostingEnvironment.EnvironmentName
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

            var host = builder.Build();
            await host.StartAsync();
            return host;
        }
    }
}
