using System;
using System.Net;
using Kritner.OrleansGettingStarted.Common.Config;
using Microsoft.Extensions.Options;
using Orleans.Configuration;
using Orleans.Hosting;

namespace Kritner.OrleansGettingStarted.SiloHost.ExtensionMethods
{
    public static class ISiloBuilderExtensions
    {
        /// <summary>
        /// Configures clustering for the Orleans Silo Host based on
        /// the Orleans environment.
        /// </summary>
        /// <param name="builder">The silo builder.</param>
        /// <param name="orleansConfig">The Orleans configuration.</param>
        /// <param name="environmentName">The environment.</param>
        public static ISiloBuilder ConfigureClustering(
            this ISiloBuilder builder,
            OrleansConfig orleansConfig,
            string environmentName
        )
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
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
