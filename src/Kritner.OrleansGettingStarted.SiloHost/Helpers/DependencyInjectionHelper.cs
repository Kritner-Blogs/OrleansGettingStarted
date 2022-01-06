using Kritner.Orleans.GettingStarted.GrainInterfaces;
using Kritner.Orleans.GettingStarted.GrainInterfaces.HealthChecks;
using Kritner.Orleans.GettingStarted.Grains;
using Kritner.Orleans.GettingStarted.Grains.HealthChecks;
using Microsoft.Extensions.DependencyInjection;

namespace Kritner.OrleansGettingStarted.SiloHost.Helpers;

/// <summary>
/// Dependency Injection helper class
/// </summary>
public static class DependencyInjectionHelper
{
    /// <summary>
    /// Register concretions for DI.
    /// </summary>
    /// <param name="serviceCollection">The service collection in which to register thingers.</param>
    public static void IocContainerRegistration(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IEmailSender, FakeEmailSender>();
    }
}