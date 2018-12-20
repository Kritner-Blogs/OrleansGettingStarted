using Kritner.OrleansGettingStarted.Client.OrleansFunctionExamples;
using Kritner.OrleansGettingStarted.Common.Config;
using Kritner.OrleansGettingStarted.Common.Helpers;
using Kritner.OrleansGettingStarted.GrainInterfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Orleans;
using Orleans.Configuration;
using Orleans.Runtime;
using System;
using System.Threading.Tasks;
using Kritner.OrleansGettingStarted.Common;
using Kritner.OrleansGettingStarted.Client.ExtensionMethods;

namespace Kritner.OrleansGettingStarted.Client
{
    public class Program
    {
        private static Startup Startup;
        private static IServiceProvider ServiceProvider;
        const int initializeAttemptsBeforeFailing = 5;
        private static int attempt = 0;

        static int Main(string[] args)
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
                using (var client = await StartClientWithRetries())
                {
                    await new OrleansExamples(new OrleansFunctionProvider())
                        .ChooseFunction(client);
                }

                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
                return 1;
            }
        }

        private static async Task<IClusterClient> StartClientWithRetries()
        {
            attempt = 0;
            IClusterClient client;
            client = new ClientBuilder()
                .ConfigureClustering(
                    ServiceProvider.GetService<IOptions<OrleansConfig>>(), 
                    Startup.HostingEnvironment.EnvironmentName
                )
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "HelloWorldApp";
                })
                .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(IGrainInterfaceMarker).Assembly).WithReferences())
                // I don't want the chatter of logging from the client for now.
                //.ConfigureLogging(logging => logging.AddConsole())
                .Build();

            await client.Connect(RetryFilter);
            Console.WriteLine("Client successfully connect to silo host");
            return client;
        }

        private static async Task<bool> RetryFilter(Exception exception)
        {
            if (exception.GetType() != typeof(SiloUnavailableException))
            {
                Console.WriteLine($"Cluster client failed to connect to cluster with unexpected error.  Exception: {exception}");
                return false;
            }
            attempt++;
            Console.WriteLine($"Cluster client attempt {attempt} of {initializeAttemptsBeforeFailing} failed to connect to cluster.  Exception: {exception}");
            if (attempt > initializeAttemptsBeforeFailing)
            {
                return false;
            }
            await Task.Delay(TimeSpan.FromSeconds(4));
            return true;
        }
    }
}
