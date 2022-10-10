using System;
using System.IO;
using Kritner.OrleansGettingStarted.Common.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Options;

namespace Kritner.OrleansGettingStarted.Common.Helpers;

public static class ConsoleAppConfigurator
{
    public static (string env, IConfigurationRoot configurationRoot, OrleansConfig orleansConfig) BootstrapConfigurationRoot()
    {
        var env = GetEnvironmentName();
        var tempConfigBuilder = new ConfigurationBuilder();

        tempConfigBuilder
            .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: false)
            .AddJsonFile($"appsettings.{env}.json", optional: false, reloadOnChange: false);

        var configurationRoot = tempConfigBuilder.Build();

        var serviceCollection = new ServiceCollection();
        serviceCollection.Configure<OrleansConfig>(configurationRoot.GetSection(nameof(OrleansConfig)));
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var orleansConfig = serviceProvider.GetService<IOptions<OrleansConfig>>().Value;
        return (env, configurationRoot, orleansConfig);
    }

    private static string GetEnvironmentName()
    {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        if (string.IsNullOrWhiteSpace(env))
        {
            return "local";
        }

        return env;
    }
}
