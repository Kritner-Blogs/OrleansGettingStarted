using Kritner.OrleansGettingStarted.Common.Config;
using Microsoft.Extensions.Options;
using Orleans.Hosting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

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

            switch (environmentName.ToLower())
            {
                case "dev":
                    builder.UseLocalhostClustering();
                    break;
                default:
                    var orleansConfig = orleansConfigOptions.Value;
                    builder.UseDevelopmentClustering(
                        new IPEndPoint(
                            IPAddress.Parse(orleansConfig.NodeIpAddresses[0]),
                            orleansConfig.SiloPort
                        )
                    );
                    break;
            }

            return builder;
        }
    }
}
