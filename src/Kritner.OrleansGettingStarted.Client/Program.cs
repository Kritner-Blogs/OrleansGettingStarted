using System;
using System.Threading.Tasks;
using Kritner.Orleans.GettingStarted.GrainInterfaces;
using Kritner.OrleansGettingStarted.Client.ExtensionMethods;
using Kritner.OrleansGettingStarted.Client.OrleansFunctionExamples;
using Kritner.OrleansGettingStarted.Common;
using Kritner.OrleansGettingStarted.Common.Config;
using Kritner.OrleansGettingStarted.Common.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Orleans;
using Orleans.Configuration;
using Orleans.Runtime;

namespace Kritner.OrleansGettingStarted.Client;

public class Program
{
    const int InitializeAttemptsBeforeFailing = 5;
    private static int _attempt;

    static async Task<int> Main(string[] args)
    {
        var (env, configurationRoot, orleansConfig) =
            ConsoleAppConfigurator.BootstrapConfigurationRoot();

        return await CreateHostBuilder(args, env, configurationRoot, orleansConfig);
    }

    private static async Task<int> CreateHostBuilder(string[] args, string env, IConfigurationRoot configurationRoot, OrleansConfig orleansConfig)
    {
        try
        {
            await using (var client = await StartClientWithRetries(env, orleansConfig))
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

    private static async Task<IClusterClient> StartClientWithRetries(string env, OrleansConfig orleansConfig)
    {
        _attempt = 0;
        IClusterClient client;
        client = new ClientBuilder()
            .ConfigureClustering(
                orleansConfig,
                env
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
        _attempt++;
        Console.WriteLine($"Cluster client attempt {_attempt} of {InitializeAttemptsBeforeFailing} failed to connect to cluster.  Exception: {exception}");
        if (_attempt > InitializeAttemptsBeforeFailing)
        {
            return false;
        }
        await Task.Delay(TimeSpan.FromSeconds(4));
        return true;
    }
}
