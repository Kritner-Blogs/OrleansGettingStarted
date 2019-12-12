using Kritner.OrleansGettingStarted.Common.Config;
using Microsoft.Extensions.Options;
using Orleans.Configuration;
using Orleans.Hosting;
using System;
using System.Net;

namespace Kritner.OrleansGettingStarted.SiloHost.ExtensionMethods
{
    public static class ISiloHostBuilderExtensions
    {
        /// <summary>
        /// Configures clustering for the Orleans Silo Host based on
        /// the Orleans environment.
        /// </summary>
        /// <param name="builder">The silo host builder.</param>
        /// <param name="orleansConfigOptions">The Orleans configuration options.</param>
        /// <param name="environmentName">The environment.</param>
        public static ISiloHostBuilder ConfigureClustering(
            this ISiloHostBuilder builder,
            IOptions<OrleansConfig> orleansConfigOptions,
            string environmentName
        )
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            if (orleansConfigOptions.Value == default(OrleansConfig))
            {
                throw new ArgumentException(nameof(orleansConfigOptions));
            }

            builder.UseLocalhostClustering();
            builder.Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback);

//            switch (environmentName.ToLower())
//            {
//                case "dev":
//                    builder.UseLocalhostClustering();
//                    builder.Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback);
//                    break;
//                default:
//                    var orleansConfig = orleansConfigOptions.Value;
//                    // Configure the first listed node as the "primary node".
//                    // Note this type of configuration should probably not be used in prod - using HA clustering instead.
//                    builder.UseDevelopmentClustering(
//                        new IPEndPoint(
//                            IPAddress.Parse(orleansConfig.NodeIpAddresses[0]),
//                            orleansConfig.SiloPort
//                        )
//                    );
//                    builder.ConfigureEndpoints(
//                        siloPort: orleansConfig.SiloPort,
//                        gatewayPort: orleansConfig.GatewayPort
//                    );
//                    break;
//            }

            return builder;
        }
    }
}
